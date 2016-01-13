using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryCo.Service;
using GroceryCo.Service.Utilities;
using GroceryCo.DTO;

namespace GroceryCo.Service.Tests
{
    [TestClass]
    public class ProductServiceTest
    {
        #region Private Variables

        IProductService productService = null;

        #endregion

        [TestInitialize]
        public void Setup()
        {
            productService = new ProductService();
        }

        [TestMethod]
        public void ShouldRetrieveProducts()
        {

            List<ProductDTO> products = productService.GetProducts();

            Assert.IsTrue(products != null && products.Count > 0, "Products have not been found");

        }
    }
}
