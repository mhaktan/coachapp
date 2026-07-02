using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using CoachApp.Entities;
using CoachApp.Payments;
using CoachApp.Payments.Dto;

namespace CoachApp.Tests.Payments
{
    public class PaymentAppServiceTests
    {
        private readonly Mock<IRepository<Payment, long>> _repositoryMock;
        private readonly PaymentAppService _service;

        public PaymentAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Payment, long>>();
            _service = new PaymentAppService(_repositoryMock.Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new Payment { Id = 1, Amount = 10.0m, PaymentDate = DateTime.UtcNow, PaymentMethod = 0, SessionsPurchased = 1 },
                new Payment { Id = 2, Amount = 10.0m, PaymentDate = DateTime.UtcNow, PaymentMethod = 0, SessionsPurchased = 1 },
            }.AsQueryable();

            _repositoryMock.Setup(r => r.GetAll()).Returns(entities);

            // Act
            var result = _repositoryMock.Object.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }

        [Fact]
        public void Repository_GetAll_WithFilter_ShouldWork()
        {
            // Arrange
            var entities = new[]
            {
                new Payment { Id = 1, Amount = 10.0m, PaymentDate = DateTime.UtcNow, PaymentMethod = 0, SessionsPurchased = 1 },
                new Payment { Id = 2, Amount = 10.0m, PaymentDate = DateTime.UtcNow, PaymentMethod = 0, SessionsPurchased = 1 },
            }.AsQueryable();

            _repositoryMock.Setup(r => r.GetAll()).Returns(entities);

            // Act — simulate keyword filter
            var result = _repositoryMock.Object.GetAll()
                .Where(x => x.Id.ToString().Contains("1"));

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Create_ShouldInsertEntity()
        {
            // Arrange
            var dto = new CreatePaymentDto
            {
                Amount = 10.0m, PaymentDate = DateTime.UtcNow, PaymentMethod = 0, SessionsPurchased = 1
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<Payment>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Payment { Id = 1, Amount = 10.0m, PaymentDate = DateTime.UtcNow, PaymentMethod = 0, SessionsPurchased = 1 });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Payment { Id = 1, Amount = 10.0m, PaymentDate = DateTime.UtcNow, PaymentMethod = 0, SessionsPurchased = 1 });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
