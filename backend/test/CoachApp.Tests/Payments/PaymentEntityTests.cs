using System;
using Xunit;
using FluentAssertions;
using CoachApp.Entities;

namespace CoachApp.Tests.Payments
{
    public class PaymentEntityTests
    {
        [Fact]
        public void Payment_ShouldBeCreatable()
        {
            // Act
            var entity = new Payment();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void Payment_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new Payment();

            // Assert
            entity.Id.Should().Be(default(long));

        }


    }
}
