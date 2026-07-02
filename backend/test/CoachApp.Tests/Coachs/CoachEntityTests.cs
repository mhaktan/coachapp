using System;
using Xunit;
using FluentAssertions;
using CoachApp.Entities;

namespace CoachApp.Tests.Coachs
{
    public class CoachEntityTests
    {
        [Fact]
        public void Coach_ShouldBeCreatable()
        {
            // Act
            var entity = new Coach();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void Coach_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new Coach();

            // Assert
            entity.Id.Should().Be(default(long));
            entity.IsActive.Should().Be(false);
        }

        [Fact]
        public void Coach_FirstName_ShouldAcceptValue()
        {
            var entity = new Coach { FirstName = "Test Value" };
            entity.FirstName.Should().Be("Test Value");
        }

        [Fact]
        public void Coach_LastName_ShouldAcceptValue()
        {
            var entity = new Coach { LastName = "Test Value" };
            entity.LastName.Should().Be("Test Value");
        }

        [Fact]
        public void Coach_Email_ShouldAcceptValue()
        {
            var entity = new Coach { Email = "Test Value" };
            entity.Email.Should().Be("Test Value");
        }

    }
}
