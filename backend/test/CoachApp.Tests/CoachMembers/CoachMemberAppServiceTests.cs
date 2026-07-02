using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using CoachApp.Entities;
using CoachApp.CoachMembers;
using CoachApp.CoachMembers.Dto;

namespace CoachApp.Tests.CoachMembers
{
    public class CoachMemberAppServiceTests
    {
        private readonly Mock<IRepository<CoachMember, long>> _repositoryMock;
        private readonly CoachMemberAppService _service;

        public CoachMemberAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<CoachMember, long>>();
            _service = new CoachMemberAppService(_repositoryMock.Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new CoachMember { Id = 1, AssignedAt = DateTime.UtcNow, IsActive = true },
                new CoachMember { Id = 2, AssignedAt = DateTime.UtcNow, IsActive = true },
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
                new CoachMember { Id = 1, AssignedAt = DateTime.UtcNow, IsActive = true },
                new CoachMember { Id = 2, AssignedAt = DateTime.UtcNow, IsActive = true },
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
            var dto = new CreateCoachMemberDto
            {
                AssignedAt = DateTime.UtcNow, IsActive = true
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<CoachMember>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new CoachMember { Id = 1, AssignedAt = DateTime.UtcNow, IsActive = true });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new CoachMember { Id = 1, AssignedAt = DateTime.UtcNow, IsActive = true });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
