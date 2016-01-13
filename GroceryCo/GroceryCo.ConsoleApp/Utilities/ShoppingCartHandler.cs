using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GroceryCo.Service;
using GroceryCo.Service.Utilities;
using GroceryCo.DTO;

namespace GroceryCo.ConsoleApp.Utilities
{
    public class ShoppingCartHandler : IShoppingCartHandler
    {
        #region Private Variables

        private IProductService productService;
        private IOrderService orderService;
        private IReportHandler reportHandler;

        #endregion

        #region Constructors

        public ShoppingCartHandler()
            : this(new ProductService(), new OrderService(), new ReportHandler())
        {
        }

        public ShoppingCartHandler(IProductService productService, IOrderService orderService, IReportHandler reportHandler)
        {
            this.productService = productService;
            this.orderService = orderService;
            this.reportHandler = reportHandler;
        }

        #endregion

        #region Public Methods

        public void CheckoutShoppingCart()
        {
            // get the shopping cart

            ShoppingCartDTO shoppingCart = GetShoppingCart();

            // get the list of products

            List<ProductDTO> products = productService.GetProducts();

            // turn the shopping cart into an order

            OrderDTO order = new OrderDTO { CreatedDate = DateTime.Now, Status = Constants.OrderStatus.Pending };
            order.OrderItems = new List<OrderItemDTO>();

            List<ShoppingCartItemDTO> orderedCart = shoppingCart.ShoppingCartItems.OrderBy(c => c.ProductCode).ToList();

            int productItemCount = 0;
            string lastProductCode = "";

            foreach (ShoppingCartItemDTO cartItem in orderedCart)
            {
                bool promotionApplied = false;

                // validate the product
                if (!products.Exists(p => p.Code == cartItem.ProductCode))
                {
                    Console.WriteLine("Product in cart is not valid: " + cartItem.ProductCode);
                    break;
                }

                // get the product

                ProductDTO product = products.First(p => p.Code == cartItem.ProductCode);

                if (product.Code != lastProductCode)
                {
                    productItemCount = 1;
                    lastProductCode = product.Code;
                }
                else
                    productItemCount++;

                // set the price (including a discount)

                decimal price = GetPrice(product, ref productItemCount, ref promotionApplied, order);

                // add the order item

                OrderItemDTO orderItem = new OrderItemDTO
                {
                    CreateDate = DateTime.Now,
                    ProductCode = cartItem.ProductCode,
                    Price = price,
                    PromotionApplied = promotionApplied
                };

                order.OrderItems.Add(orderItem);
            }

            if (order.OrderItems.Count == 0) return;

            // save the order

            order.Status = Constants.OrderStatus.Complete;

            order = orderService.Save(order);

            // print the invoice

            DisplayInvoice(order);

        }

        #endregion

        #region Private Methods

        private ShoppingCartDTO GetShoppingCart()
        {
            // turn the shopping cart file into a shopping cart

            var lines = File.ReadAllLines("ShoppingCart.txt");

            ShoppingCartDTO shoppingCart = new ShoppingCartDTO();
            shoppingCart.ShoppingCartItems = new List<ShoppingCartItemDTO>();

            foreach (string line in lines)
            {
                if (!String.IsNullOrWhiteSpace(line.Trim()))
                {
                    ShoppingCartItemDTO cartItem = new ShoppingCartItemDTO { ProductCode = line };
                    shoppingCart.ShoppingCartItems.Add(cartItem);
                }
            }

            return shoppingCart;
        }

        private decimal GetPrice(ProductDTO product, ref int productItemCount, ref bool promotionApplied, OrderDTO order)
        {
            // set the price (including a discount)

            decimal price = product.Price;

            if (product.Promotions.Count > 0)
            {
                // assume the service only return 1 active promotion
                PromotionDTO promotion = product.Promotions.First();

                if (promotion.PromotionCode == Constants.PromotionType.Discount)
                {
                    // we reset the productItemCount once the promotion reaches the max apply to count
                    if (promotion.ApplyTo != null && productItemCount > promotion.Quantity + promotion.ApplyTo)
                        productItemCount = 1;

                    // set the price by the percentage discount
                    if (productItemCount > promotion.Quantity)
                    {
                        price = price * promotion.Amount;
                        promotionApplied = true;
                    }
                }
                else if (promotion.PromotionCode == Constants.PromotionType.Price && productItemCount == promotion.Quantity)
                {
                    // for a price by a number of items, we set the price on the final item in the group, and set
                    // all other items in the group to a price of 0

                    for (int i = order.OrderItems.Count - 1; i >= order.OrderItems.Count - promotion.Quantity + 1; i--)
                    {
                        OrderItemDTO orderItemPriceAdjustment = order.OrderItems.ToList()[i];
                        orderItemPriceAdjustment.Price = 0;
                        orderItemPriceAdjustment.PromotionApplied = true;
                    }

                    price = promotion.Amount;
                    productItemCount = 0;
                    promotionApplied = true;
                }

            }

            return price;
        }

        private void DisplayInvoice(OrderDTO order)
        {
            List<ProductDTO> products = productService.GetProducts();

            Console.WriteLine("".PadRight(30, '-') + "RECEIPT" + "".PadRight(30, '-'));

            Console.WriteLine();
            Console.WriteLine("Order Number: " + order.Id);
            Console.WriteLine("Date: " + order.CreatedDate.ToString());
            Console.WriteLine();
            Console.WriteLine();

            reportHandler.SetupColumns(30, 10, 30);

            reportHandler.AddColumns("Product", "Price", "Promotion Applied");

            reportHandler.AddColumns('-', "", "", "");

            decimal total = 0;

            foreach (OrderItemDTO orderItem in order.OrderItems)
            {
                ProductDTO product = products.First(p => p.Code == orderItem.ProductCode);

                reportHandler.AddColumns(product.Description, orderItem.Price.ToString("C"), 
                    (orderItem.PromotionApplied ? "Yes" : "No"));

                total += orderItem.Price;
            }

            reportHandler.AddRow();

            reportHandler.AddColumns("Total", total.ToString("C"));

            reportHandler.GenerateReport();

        }

        #endregion

    }
}
