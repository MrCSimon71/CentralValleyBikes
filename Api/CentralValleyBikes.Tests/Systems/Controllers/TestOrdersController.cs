using CentralValleyBikes.Api.Controllers;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CentralValleyBikes.Tests.Systems.Controllers
{
    public class TestOrdersController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnsStatusCode200()
        {
            // Arrange
            var mockOrderService = new Mock<IOrderService<Order, int>>();

            mockOrderService
                .Setup(svc => svc.GetAllAsync())
                .ReturnsAsync(new List<Order>());

            var sut = new OrdersController(mockOrderService.Object);

            // Act
            var result = (OkObjectResult)await sut.Get();

            // Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokesOrderService()
        {
            // Arrange
            var mockOrderService = new Mock<IOrderService<Order, int>>();

            mockOrderService
                .Setup(svc => svc.GetAllAsync())
                .ReturnsAsync(new List<Order>());

            var sut = new OrdersController(mockOrderService.Object);

            // Act
            var result = await sut.Get();

            // Assert
            mockOrderService.Verify(svc => svc.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Get_OnSuccess_ReturnsListOfOrders()
        {
            // Arrange
            var mockOrderService = new Mock<IOrderService<Order, int>>();

            mockOrderService
                .Setup(svc => svc.GetAllAsync())
                .ReturnsAsync(new List<Order>()
                {
                    new()
                    {
                        OrderId = 1,
                    },
                    new()
                    {
                        OrderId = 2
                    }
                });

            var sut = new OrdersController(mockOrderService.Object);

            // Act
            var result = await sut.Get();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<Order>>();
        }

        [Fact]
        public async Task Get_OnNoOrdersFound_Returns404()
        {
            // Arrange
            var mockOrderService = new Mock<IOrderService<Order, int>>();

            mockOrderService
                .Setup(svc => svc.GetAllAsync())
                .ReturnsAsync(new List<Order>());

            var sut = new OrdersController(mockOrderService.Object);

            // Act
            var result = await sut.Get();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}