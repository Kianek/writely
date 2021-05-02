using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Writely.Controllers;
using Writely.Exceptions;
using Writely.Models;
using Writely.Services;
using Xunit;

namespace Writely.UnitTests.Controllers
{
    public class UsersControllerTests
    {
        [Fact]
        public async Task Register_NewUser_ReturnsCreated()
        {
            // Arrange
            var registration = CompleteRegistration();
            var controller = PrepControllerWithoutUser();

            // Act
            var response = await controller.Register(registration);

            // Assert
            response.Should().BeOfType<OkResult>();
        }
        
        [Fact]
        public async Task Register_IncompleteRegistration_ReturnsBadRequest()
        {
            // Arrange
            var registration = IncompleteRegistration();
            var controller = PrepControllerForIncompleteInfo();

            // Act
            var response = await controller.Register(registration);

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }
        
        [Fact]
        public async Task Register_UserAlreadyRegistered_ReturnsBadRequest()
        {
            // Arrange
            var registration = Helpers.GetRegistration();
            var controller = PrepControllerWithUser();

            // Act
            var response = await controller.Register(registration);

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }
        
        [Fact]
        public async Task ChangeEmail_ChangeSuccessful_ReturnsOk()
        {
            // Arrange
            var emailUpdate = Helpers.GetEmailUpdate();
            var controller = PrepControllerWithUser();

            // Act
            var response = await controller.ChangeEmail(emailUpdate);

            // Assert
            response.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task ChangeEmail_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var emailUpdate = Helpers.GetEmailUpdate();
            var controller = PrepControllerWithoutUser();

            // Act
            var response = await controller.ChangeEmail(emailUpdate);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task ChangeEmail_AccountUpdateNull_ReturnsBadRequest()
        {
            // Arrange
            var controller = PrepControllerForIncompleteInfo();

            // Act
            var response = await controller.ChangeEmail(null!);

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }
        
        [Fact]
        public async Task ChangeEmail_IncompleteAccountUpdate_ReturnsBadRequest()
        {
            // Arrange
            var accountUpdate = Helpers.GetEmailUpdate();
            accountUpdate.EmailUpdate!.Email = null;
            var controller = PrepControllerForIncompleteInfo();

            // Act
            var response = await controller.ChangeEmail(accountUpdate);

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task ChangeEmail_NewEmailSameAsOld_ReturnsBadRequest()
        {
            // Arrange
            var accountUpdate = Helpers.GetEmailUpdate("old@email.com");
            var mockUserService = GetMockUserService();
            mockUserService.Setup(us =>
                    us.ChangeEmail(It.IsAny<AccountUpdate>()))
                .ReturnsAsync(() => IdentityResult.Failed());
            var controller = new UsersController(mockUserService.Object);

            // Act
            var response = await controller.ChangeEmail(accountUpdate);

            // Assert
            response.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public async Task ChangePassword_ChangeSuccessful_ReturnsOk()
        {
            // Arrange
            var accountUpdate = Helpers.GetPasswordUpdate();
            var controller = PrepControllerWithUser();

            // Act
            var response = await controller.ChangePassword(accountUpdate);

            // Assert
            response.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task ChangePassword_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var passwordUpdate = Helpers.GetPasswordUpdate();
            var controller = PrepControllerWithoutUser();

            // Act
            var response = await controller.ChangeEmail(passwordUpdate);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task ChangePassword_PasswordUpdateNull_ReturnsBadRequest()
        {
            // Arrange
            var accountUpdate = Helpers.GetPasswordUpdate();
            accountUpdate.PasswordUpdate = null;
            var controller = PrepControllerForIncompleteInfo();

            // Act
            var response = await controller.ChangePassword(accountUpdate);

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }
        
        [Fact]
        public async Task ChangePassword_IncompleteAccountUpdate_ReturnsBadRequest()
        {
            // Arrange
            var accountUpdate = Helpers.GetPasswordUpdate();
            accountUpdate.PasswordUpdate!.CurrentPassword = "Goobledyblech";
            var service = GetMockUserService();
            service.Setup(us => us.ChangePassword(accountUpdate))
                .Throws<PasswordMismatchException>();
            var controller = new UsersController(service.Object);

            // Act
            var response = await controller.ChangePassword(accountUpdate);

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }
        
        [Fact]
        public async Task DeleteAccount_AccountDeleted_ReturnsOk()
        {
            // Arrange
            var controller = PrepControllerWithUser();

            // Act
            var response = await controller.DeleteAccount("UserId");

            // Assert
            response.Should().BeOfType<OkResult>();
        }
        
        [Fact]
        public async Task DeleteAccount_AccountNotFound_ReturnsBadRequest()
        {
            // Arrange
            var controller = PrepControllerWithUser();

            // Act
            var response = await controller.DeleteAccount("UserIdDelete");

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        private Registration CompleteRegistration()
            => new()
            {
                Username = "fancy.mcfancypants",
                Email = "fancy@gmail.com",
                FirstName = "Fancy",
                LastName = "McFancypants",
                Password = "SuperSecretPassword123!",
                ConfirmPassword = "SuperSecretPassword123!"
            };

        private Registration IncompleteRegistration() => new();

        private UsersController PrepControllerWithoutUser()
        {
            var userService = GetMockUserService();
            userService.Setup(us => us.CreateAccount(It.IsAny<Registration>()))
                .ReturnsAsync(() => IdentityResult.Success);
            userService.Setup(us => us.CreateAccount(IncompleteRegistration()))
                .Throws<IncompleteRegistrationException>();
            userService.Setup(us => us.ChangeEmail(It.IsAny<AccountUpdate>()))
                .Throws<UserNotFoundException>();
            userService.Setup(us => us.ChangePassword(It.IsAny<AccountUpdate>()))
                .Throws<UserNotFoundException>();
            
            return new (userService.Object);
        }
        
        private UsersController PrepControllerWithUser()
        {
            var userService = GetMockUserService();
            userService.Setup(us => us.CreateAccount(It.IsAny<Registration>()))
                .Throws<DuplicateUserException>();
            userService.Setup(us => us.ChangeEmail(It.IsAny<AccountUpdate>()))
                .ReturnsAsync(() => IdentityResult.Success);
            userService.Setup(us => us.ChangePassword(It.IsAny<AccountUpdate>()))
                .ReturnsAsync(() => IdentityResult.Success);
            userService.Setup(us => us.DeleteAccount("UserId"))
                .ReturnsAsync(() => IdentityResult.Success);
            userService.Setup(us => us.DeleteAccount("UserIdDelete"))
                .Throws<UserNotFoundException>();
            
            return new (userService.Object);
        }

        private UsersController PrepControllerForIncompleteInfo()
        {
            var userService = GetMockUserService();
            userService.Setup(us => us.CreateAccount(It.IsAny<Registration>()))
                .Throws<MissingInformationException>();
            userService.Setup(us => us.ChangeEmail(It.IsAny<AccountUpdate>()))
                .Throws<MissingInformationException>();
            userService.Setup(us => us.ChangePassword(It.IsAny<AccountUpdate>()))
                .Throws<MissingInformationException>();
            
            return new (userService.Object);
        }

        private Mock<IUserService> GetMockUserService() => new ();
    }
}