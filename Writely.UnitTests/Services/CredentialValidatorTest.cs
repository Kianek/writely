using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Writely.Data;
using Writely.Models;
using Writely.Services;
using Xunit;

namespace Writely.UnitTests.Services
{
    public class CredentialValidatorTest
    {
        private ICredentialValidator? _validator;
        private Mock<UserManager<AppUser>> _userManager;

        public CredentialValidatorTest()
        {
            _userManager = Helpers.GetMockUserManager();
        }

        [Fact]
        public async Task AreValidCredentials_UserFound_ReturnsTrue()
        {
            // Arrange
            PrepValidator();
            
            // Act
            var result = await _validator!.AreValidCredentials(Helpers.GetCredentials());

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task AreValidCredentials_UserNotFound_ReturnsFalse()
        {
            // Arrange
            PrepValidator(false);
            
            // Act
            var result = await _validator!.AreValidCredentials(Helpers.GetCredentials());

            // Assert
            result.Should().BeFalse();
        }

        private void PrepValidator(bool testSuccessful = true)
        {
            if (testSuccessful)
            {
                PrepUserManagerForSuccessfulResult();
            }
            else
            {
                PrepUserManagerForFailedResult();
            }
            
            _validator = new CredentialValidator(_userManager.Object);
        }

        private void PrepUserManagerForSuccessfulResult()
        {
            _userManager.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new AppUser());
            _userManager.Setup(um
                    => um.CheckPasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);
        }

        private void PrepUserManagerForFailedResult()
        {
            _userManager.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null!);
            _userManager.Setup(um
                    => um.CheckPasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(false);
        }
    }
}