using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryCo.Data;
using GroceryCo.DTO;

namespace GroceryCo.Service
{
    public class ProductService : IProductService
    {
        #region Private Variables

        private GroceryCoEntities db;

        #endregion

        #region Constructors

        public ProductService()
            : this(new GroceryCoEntities())
        {
        }

        public ProductService(GroceryCoEntities db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a list of all products
        /// </summary>
        /// <returns>Returns all products and any active promotions</returns>
        public List<ProductDTO> GetProducts()
        {
            var result = db.Products.OrderBy(p => p.Description);

            List<ProductDTO> productsDTO = new List<ProductDTO>();

            foreach(Product product in result.ToList())
            {
                // get the product DTO, and only return promotions that have not expired
                ProductDTO productDTO = new ProductDTO
                {
                    Code = product.Code,
                    Description = product.Description,
                    ProductTypeCode = product.ProductTypeCode,
                    Price = product.Price,
                    Promotions = ConvertPromotionList(product.Promotions.Where(p => p.EndDate == null).ToList())
                };

                productsDTO.Add(productDTO);
            }

            return productsDTO;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Converts an Entity Framework list of promotions to a Promotion DTO
        /// </summary>
        /// <param name="promotions"></param>
        /// <returns>As Promotion DTO list</returns>
        private List<PromotionDTO> ConvertPromotionList(List<Promotion> promotions)
        {
            List<PromotionDTO> promotionsDTO = new List<PromotionDTO>();

            foreach(Promotion promotion in promotions)
            {
                PromotionDTO promotionDTO = new PromotionDTO
                {
                    Amount = promotion.Amount,
                    ApplyTo = promotion.ApplyTo,
                    EndDate = promotion.EndDate,
                    Id = promotion.Id,
                    PromotionCode = promotion.PromotionCode,
                    ProductCode = promotion.PromotionCode,
                    Quantity = promotion.Quantity,
                    StartDate = promotion.StartDate
                };

                promotionsDTO.Add(promotionDTO);
            }

            return promotionsDTO;
        }

        #endregion

    }
}
