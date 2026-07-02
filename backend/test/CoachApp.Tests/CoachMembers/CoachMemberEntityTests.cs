using System;
using Xunit;
using FluentAssertions;
using CoachApp.Entities;

namespace CoachApp.Tests.CoachMembers
{
    public class CoachMemberEntityTests
    {
        [Fact]
        public void CoachMember_ShouldBeCreatable()
        {
            // Act
            var entity = new CoachMember();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void CoachMember_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new CoachMember();

            // Assert
            entity.Id.Should().Be(default(long));
            entity.IsActive.Should().Be(false);
        }


    }
}
