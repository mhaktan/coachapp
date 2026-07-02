using System;
using Xunit;
using FluentAssertions;
using CoachApp.Entities;

namespace CoachApp.Tests.Attendances
{
    public class AttendanceEntityTests
    {
        [Fact]
        public void Attendance_ShouldBeCreatable()
        {
            // Act
            var entity = new Attendance();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void Attendance_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new Attendance();

            // Assert
            entity.Id.Should().Be(default(long));
            entity.Attended.Should().Be(false);
            entity.SessionDeducted.Should().Be(false);
        }


    }
}
