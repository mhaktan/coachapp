using System;
using Xunit;
using FluentAssertions;
using CoachApp.Entities;

namespace CoachApp.Tests.SessionPackages
{
    public class SessionPackageEntityTests
    {
        [Fact]
        public void SessionPackage_ShouldBeCreatable()
        {
            // Act
            var entity = new SessionPackage();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void SessionPackage_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new SessionPackage();

            // Assert
            entity.Id.Should().Be(default(long));
            entity.IsActive.Should().Be(false);
        }

        [Fact]
        public void SessionPackage_PackageName_ShouldAcceptValue()
        {
            var entity = new SessionPackage { PackageName = "Test Value" };
            entity.PackageName.Should().Be("Test Value");
        }

    }
}
