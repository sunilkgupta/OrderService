using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Order.API.Controllers;
using Order.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.UnitTest
{
    [TestClass]
    public class OrderControllerUnitTest
    {
        private Mock<ILogger<OrderController>> _logger;
        private Mock<IOrderBusiness> mockOrderBusiness;
        private Fixture _fixture;

        [TestInitialize]
        public void OrderControllerUnitTestTestInitialize()
        {
            _logger = new Mock<ILogger<OrderController>>();
            mockOrderBusiness = new Mock<IOrderBusiness>();
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

        }

        [TestMethod]
        public async Task OrderController_Get_All_Orders()
        {
            //Arrange
            var orderResult = _fixture.Create<Task<IEnumerable<Common.Entities.Order>>>();
            mockOrderBusiness.Setup(c => c.GetOrders()).Returns(orderResult);

            //Act
            var orderController = new OrderController(_logger.Object, mockOrderBusiness.Object);
            var result = await orderController.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(orderResult);
        }

        [TestMethod]
        public async Task OrderController_Get_All_Orders_Null()
        {
            //Arrange
            var orderResult = _fixture.Create<Task<IEnumerable<Common.Entities.Order>>>();
            mockOrderBusiness.Setup(c => c.GetOrders()).Returns(orderResult);
            orderResult = null;

            //Act
            var orderController = new OrderController(_logger.Object, mockOrderBusiness.Object);
            var result = await orderController.Get();
            result = null;

            //Assert
            Assert.IsNull(result);
            Assert.IsNull(orderResult);
        }

        [TestMethod]
        public async Task OrderController_Get_Order_By_Id()
        {
            //Arrange
            var orderResult = _fixture.Create<Task<Common.Entities.Order>>();
            mockOrderBusiness.Setup(c => c.GetOrdersById(It.IsAny<Guid>())).Returns(orderResult);
            //orderResult = null;

            //Act
            var orderController = new OrderController(_logger.Object, mockOrderBusiness.Object);
            var id = _fixture.Create<Guid>();
            var result = await orderController.Get(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(orderResult);
        }

        [TestMethod]
        public async Task OrderController_Create_Order()
        {
            //Arrange
            var orderResult = _fixture.Create<Task<Common.Entities.Order>>();
            orderResult.Result.OrderId = Guid.Empty;
            mockOrderBusiness.Setup(c => c.CreateOrder(It.IsAny<Common.Entities.Order>())).Returns(orderResult);

            //Act
            var orderController = new OrderController(_logger.Object, mockOrderBusiness.Object);
            var order = _fixture.Create<Common.Entities.Order>();
            var result = await orderController.Post(order);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(orderResult);
        }

        [TestMethod]
        public async Task OrderController_Update_Order()
        {
            //Arrange
            var orderResult = _fixture.Create<Task<Common.Entities.Order>>();
            mockOrderBusiness.Setup(c => c.UpdateOrder(It.IsAny<Guid>(),It.IsAny<Common.Entities.Order>())).Returns(orderResult);

            //Act
            var orderController = new OrderController(_logger.Object, mockOrderBusiness.Object);
            var order = _fixture.Create<Common.Entities.Order>();
            var id = _fixture.Create<Guid>();

            var result = await orderController.Put(id,order);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(orderResult);
        }

        [TestMethod]
        public async Task OrderController_Delete_Order()
        {
            //Arrange
            var orderResult = _fixture.Create<Task<bool>>();
            mockOrderBusiness.Setup(c => c.DeleteOrder(It.IsAny<Guid>())).Returns(orderResult);

            //Act
            var orderController = new OrderController(_logger.Object, mockOrderBusiness.Object);
            var id = _fixture.Create<Guid>();

            var result = await orderController.Delete(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(orderResult);
        }        
    }
}