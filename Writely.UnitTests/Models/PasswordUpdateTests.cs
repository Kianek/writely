using FluentAssertions;
using Writely.Models;
using Xunit;

namespace Writely.UnitTests.Models
{
    public class PasswordUpdateTests
    {
        [Fact]
        public void PasswordsMatch_ReturnsTrue()
        {
            // Arrange
            var update = GetPasswordUpdate("SpiffyPassword123", "SpiffyPassword123");

            // Assert
            update.PasswordsMatch().Should().BeTrue();
        }

        [Fact]
        public void PasswordsMatch_PasswordsDifferent_ReturnsFalse()
        {
            // Arrange
            var update = GetPasswordUpdate("SpiffyPassword123", "LessSpiffyPassword123");

            // Assert
            update.PasswordsMatch().Should().BeFalse();
        }

        [Fact]
        public void PasswordsMatch_NullProperty_ReturnsFalse()
        {
            // Arrange
            var update = GetPasswordUpdate("LonelyPassword123");

            // Assert
            update.PasswordsMatch().Should().BeFalse();
        }

        private PasswordUpdate GetPasswordUpdate(string? password = null, string? confirmPassword = null)
            => new () {Password = password, ConfirmPassword = confirmPassword};
    }
}