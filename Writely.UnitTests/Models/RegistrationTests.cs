using FluentAssertions;
using Writely.Models;
using Xunit;

namespace Writely.UnitTests.Models
{
    public class RegistrationTests
    {
        [Fact]
        public void IsComplete_AllPropertiesPresent_ReturnsTrue()
        {
            // Arrange
            var registration = new Registration
            {
                Username = "bob.loblaw",
                Email = "bob@gmail.com",
                FirstName = "Bob",
                LastName = "Loblaw",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            };

            // Assert
            registration.IsComplete().Should().BeTrue();
        }

        [Fact]
        public void IsComplete_SomePropertiesMissing_ReturnsFalse()
        {
            // Arrange
            var registration = new Registration
            {
                Username = "bob.loblaw",
                Email = "bob@gmail.com",
                Password = "Password123!"
            };
            
            // Assert
            registration.IsComplete().Should().BeFalse();
        }
    }
}