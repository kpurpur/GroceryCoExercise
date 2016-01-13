using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryCo.Data;
using GroceryCo.DTO;

namespace GroceryCo.Service
{
    public class OrderService : IOrderService
    {
        #region Private Variables

        private GroceryCoEntities db;

        #endregion

        #region Constructors

        public OrderService()
            : this(new GroceryCoEntities())
        {
        }

        public OrderService(GroceryCoEntities db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get an Order by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order and Order Items</returns>
        public OrderDTO Get(long id)
        {
            var result = db.Orders.Where(o => o.Id == id);

            if (result.Count() == 0)
                return null;

            Order order = result.ToList()[0];

            // transform the order DTO

            OrderDTO orderDTO = new OrderDTO()
            {
                Id = order.Id,
                CreatedDate = order.CreatedDate,
                ModifiedDate = order.ModifiedDate,
                Status = order.Status
            };

            // transform the order item DTOs

            orderDTO.OrderItems = new List<OrderItemDTO>();

            foreach(OrderItem orderItem in order.OrderItems)
            {
                OrderItemDTO orderItemDTO = new OrderItemDTO()
                {
                    Id = orderItem.Id,
                    CreateDate = orderItem.CreateDate,
                    ModifiedDate = orderItem.ModifiedDate,
                    OrderId = orderItem.OrderId,
                    Price = orderItem.Price,
                    ProductCode = orderItem.ProductCode,
                    PromotionApplied = orderItem.PromotionApplied 
                };
                orderDTO.OrderItems.Add(orderItemDTO);
            }

            return orderDTO;
        }

        /// <summary>
        /// Saves an Order and it's Order Items
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns>Returns the saved order and order items (including new Id's)</returns>
        public OrderDTO Save(OrderDTO orderDTO)
        {
            Order order;

            // transform an order DTO to an order (entity framework)

            if (orderDTO.Id == 0)
            {
                order = new Order { CreatedDate = orderDTO.CreatedDate };
                db.Orders.Add(order);
            }
            else
            {
                order = db.Orders.Find(orderDTO.Id);
                order.ModifiedDate = orderDTO.ModifiedDate;
            }

            order.Status = orderDTO.Status;

            foreach(OrderItemDTO orderItemDTO in orderDTO.OrderItems)
            {
                // transform an order item DTO to an order item (entity framework)

                OrderItem orderItem;

                if (orderItemDTO.Id == 0)
                {
                    orderItem = new OrderItem { CreateDate = orderItemDTO.CreateDate };
                    order.OrderItems.Add(orderItem);
                }
                else
                {
                    orderItem = order.OrderItems.First(i => i.Id == orderItemDTO.Id);
                    orderItem.ModifiedDate = orderItemDTO.ModifiedDate;
                }

                orderItem.ProductCode = orderItemDTO.ProductCode;
                orderItem.Price = orderItemDTO.Price;
                orderItem.PromotionApplied = orderItemDTO.PromotionApplied;
            }

            db.SaveChanges();

            // retrieve and return the saved dto (to get all Id's, including children)
            return Get(order.Id);
        }

        #endregion

    }
}
