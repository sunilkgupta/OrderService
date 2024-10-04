using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Order.API.Controllers;
using OrderService.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.UnitTest
{
    [TestClass]
    public class OrderControllerUnitTest
    {
        private readonly Mock<ILogger<OrderController>> _logger;
        private readonly Mock<OrderAPIContext> _context;
        private readonly IFixture _fixture;
        public OrderControllerUnitTest(IFixture fixture)
        {
            _fixture = fixture;
            _logger = new Mock<ILogger<OrderController>>();
            _context = new Mock<OrderAPIContext>();

        }

        [TestMethod]
        [DataRow("18B3ADB0-545E-4351-AA70-3B83710FB699")]
        public async Task OrderController_Get_By_Id_When_Order_Is_Null(Guid id)
        {
            //Arrange
            var orderController = new OrderController(_logger.Object, _context.Object);
            DbSet<API.Models.Order> listOrders = null;

            _context.Setup(c => c.Order).Returns(listOrders);

            //orderController.Setup(c => c.Get(It.IsAny<Guid>())).Returns(It.IsAny<Task<List<API.Models.Order>>>);

            //Act
            var result = await orderController.Get(id);

            //Assert
            Assert.IsNull(listOrders);
            Assert.IsNull(result);
        }

        [TestMethod]
        [DataRow("18B3ADB0-545E-4351-AA70-3B83710FB699")]
        public async Task OrderController_Get_By_Id_When_Order_Is_Not_Null(Guid id)
        {
            //Arrange
            var orderController = new OrderController(_logger.Object, _context.Object);
            var listOrders = _fixture.Create<DbSet<API.Models.Order>>();

            _context.Setup(c => c.Order).Returns(listOrders);

            //orderController.Setup(c => c.Get(It.IsAny<Guid>())).Returns(It.IsAny<Task<List<API.Models.Order>>>);

            //Act
            var result = await orderController.Get(id);

            //Assert
            Assert.IsNotNull(listOrders);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [DataRow("18B3ADB0-545E-4351-AA70-3B83710FB699")]
        public async Task OrderController_Get_By_Id_When_Order_Is_Not_Null_But_Output_Is_Null(Guid id)
        {
            //Arrange
            var orderController = new OrderController(_logger.Object, _context.Object);
            var listOrders = _fixture.Create<DbSet<API.Models.Order>>();

            _context.Setup(c => c.Order).Returns(listOrders);

            //orderController.Setup(c => c.Get(It.IsAny<Guid>())).Returns(It.IsAny<Task<List<API.Models.Order>>>);

            //Act
            var result = await orderController.Get(id);
            result = null;

            //Assert
            Assert.IsNotNull(listOrders);
            Assert.IsNull(result);
        }

        [TestMethod]
        [DataRow("18B3ADB0-545E-4351-AA70-3B83710FB699")]
        public void OrderController_Get_When_Throw_Exception(Guid id)
        {
            //Arrange
            var inventoryController = new OrderController(_logger.Object, _context.Object);
            var getException = _fixture.Create<Exception>();

            //Act
            var result = Assert.ThrowsException<Exception>(() => inventoryController.Get(id));
            Assert.AreEqual(getException, result);

        }
    }
}