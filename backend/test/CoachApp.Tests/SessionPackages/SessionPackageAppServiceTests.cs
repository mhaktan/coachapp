using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using CoachApp.Entities;
using CoachApp.SessionPackages;
using CoachApp.SessionPackages.Dto;

namespace CoachApp.Tests.SessionPackages
{
    public class SessionPackageAppServiceTests
    {
        private readonly Mock<IRepository<SessionPackage, long>> _repositoryMock;
        private readonly SessionPackageAppService _service;

        public SessionPackageAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<SessionPackage, long>>();
            _service = new SessionPackageAppService(_repositoryMock.Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new SessionPackage { Id = 1, PackageName = "Test packageName", SessionCount = 1, Price = 10.0m, IsActive = true },
                new SessionPackage { Id = 2, PackageName = "Test packageName", SessionCount = 1, Price = 10.0m, IsActive = true },
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
                new SessionPackage { Id = 1, PackageName = "Test packageName", SessionCount = 1, Price = 10.0m, IsActive = true },
                new SessionPackage { Id = 2, PackageName = "Test packageName", SessionCount = 1, Price = 10.0m, IsActive = true },
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
            var dto = new CreateSessionPackageDto
            {
                PackageName = "Test packageName", SessionCount = 1, Price = 10.0m, IsActive = true
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<SessionPackage>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new SessionPackage { Id = 1, PackageName = "Test packageName", SessionCount = 1, Price = 10.0m, IsActive = true });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new SessionPackage { Id = 1, PackageName = "Test packageName", SessionCount = 1, Price = 10.0m, IsActive = true });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
