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
    public class OrderServiceTest
    {
        #region Private Variables

        IOrderService orderService = null;

        #endregion

        [TestInitialize]
        public void Setup()
        {
            orderService = new OrderService();
        }

        [TestMethod]
        public void ShouldCreateOrder()
        {

            // save the order

            OrderDTO orderDTO = new OrderDTO() { CreatedDate = DateTime.Now, Status = Constants.OrderStatus.Pending };

            orderDTO.OrderItems = new List<OrderItemDTO>();

            OrderItemDTO orderItemAppleDTO = new OrderItemDTO() { CreateDate = DateTime.Now, Price = 1, ProductCode = "apple" };
            orderDTO.OrderItems.Add(orderItemAppleDTO);

            OrderItemDTO orderItemNutellaDTO = new OrderItemDTO() { CreateDate = DateTime.Now, Price = 5, ProductCode = "nuttl" };
            orderDTO.OrderItems.Add(orderItemNutellaDTO);

            orderDTO = orderService.Save(orderDTO);

            Assert.IsTrue(orderDTO != null && orderDTO.Id > 0, "Order has not been saved");

            Assert.IsTrue(orderDTO.OrderItems != null && orderDTO.OrderItems.Count == 2, "Order items have not been saved");

        }
    }
}
