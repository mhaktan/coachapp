using System;
using Xunit;
using FluentAssertions;
using CoachApp.Entities;

namespace CoachApp.Tests.Lessons
{
    public class LessonEntityTests
    {
        [Fact]
        public void Lesson_ShouldBeCreatable()
        {
            // Act
            var entity = new Lesson();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void Lesson_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new Lesson();

            // Assert
            entity.Id.Should().Be(default(long));

        }


    }
}
