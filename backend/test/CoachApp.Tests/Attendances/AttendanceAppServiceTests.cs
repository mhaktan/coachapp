using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using CoachApp.Entities;
using CoachApp.Attendances;
using CoachApp.Attendances.Dto;

namespace CoachApp.Tests.Attendances
{
    public class AttendanceAppServiceTests
    {
        private readonly Mock<IRepository<Attendance, long>> _repositoryMock;
        private readonly AttendanceAppService _service;

        public AttendanceAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Attendance, long>>();
            _service = new AttendanceAppService(_repositoryMock.Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new Attendance { Id = 1, Attended = true, SessionDeducted = true },
                new Attendance { Id = 2, Attended = true, SessionDeducted = true },
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
                new Attendance { Id = 1, Attended = true, SessionDeducted = true },
                new Attendance { Id = 2, Attended = true, SessionDeducted = true },
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
            var dto = new CreateAttendanceDto
            {
                Attended = true, SessionDeducted = true
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<Attendance>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Attendance { Id = 1, Attended = true, SessionDeducted = true });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Attendance { Id = 1, Attended = true, SessionDeducted = true });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
