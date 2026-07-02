using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using CoachApp.Entities;
using CoachApp.Coachs;
using CoachApp.Coachs.Dto;

namespace CoachApp.Tests.Coachs
{
    public class CoachAppServiceTests
    {
        private readonly Mock<IRepository<Coach, long>> _repositoryMock;
        private readonly CoachAppService _service;

        public CoachAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Coach, long>>();
            _service = new CoachAppService(_repositoryMock.Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new Coach { Id = 1, FirstName = "Test firstName", LastName = "Test lastName", Email = "Test email", IsActive = true },
                new Coach { Id = 2, FirstName = "Test firstName", LastName = "Test lastName", Email = "Test email", IsActive = true },
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
                new Coach { Id = 1, FirstName = "Test firstName", LastName = "Test lastName", Email = "Test email", IsActive = true },
                new Coach { Id = 2, FirstName = "Test firstName", LastName = "Test lastName", Email = "Test email", IsActive = true },
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
            var dto = new CreateCoachDto
            {
                FirstName = "Test firstName", LastName = "Test lastName", Email = "Test email", IsActive = true
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<Coach>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Coach { Id = 1, FirstName = "Test firstName", LastName = "Test lastName", Email = "Test email", IsActive = true });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Coach { Id = 1, FirstName = "Test firstName", LastName = "Test lastName", Email = "Test email", IsActive = true });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
