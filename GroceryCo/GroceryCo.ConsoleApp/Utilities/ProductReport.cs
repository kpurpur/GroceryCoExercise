using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryCo.Service;
using GroceryCo.Service.Utilities;
using GroceryCo.DTO;

namespace GroceryCo.ConsoleApp.Utilities
{
    public class ProductReport : IProductReport
    {
        #region Private Variables

        private IProductService productService;
        private IReportHandler reportHandler;

        #endregion

        #region Constructors

        public ProductReport()
            : this(new ProductService(), new ReportHandler())
        {
        }

        public ProductReport(IProductService productService, IReportHandler reportHandler)
        {
            this.productService = productService;
            this.reportHandler = reportHandler;
        }

        #endregion

        #region Public Methods

        public void DisplayProducts()
        {
            List<ProductDTO> products = productService.GetProducts();

            Console.WriteLine();
            Console.WriteLine("Products:");
            Console.WriteLine();

            reportHandler.SetupColumns(30, 10, 30);

            reportHandler.AddColumns("Name", "Code", "Promotion");

            reportHandler.AddColumns('-', "", "", "");

            foreach (ProductDTO product in products)
            {
                string promotionDisplay = string.Empty;

                if (product.Promotions.Count > 0)
                {
                    // we assume the service will only return one active promotion (promotions are stored in the DB)
                    PromotionDTO promotion = product.Promotions.First();

                    if (promotion.PromotionCode == Constants.PromotionType.Discount)
                        promotionDisplay = String.Format("Buy {0}, Discount Next {1} to {2:P}", promotion.Quantity, promotion.ApplyTo, promotion.Amount);

                    else if (promotion.PromotionCode == Constants.PromotionType.Price)
                        promotionDisplay = String.Format("Buy {0} for {1:C}", promotion.Quantity, promotion.Amount);
                }

                reportHandler.AddColumns(product.Description, product.Code, promotionDisplay);
            }

            reportHandler.GenerateReport();
        }

        #endregion
    }
}
