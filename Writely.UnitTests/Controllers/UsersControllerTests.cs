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
            var registration = new Registration
            {
                Username = "fancy.mcfancypants",
                Email = "fancy@gmail.com",
                FirstName = "Fancy",
                LastName = "McFancypants",
                Password = "SuperSecretPassword123!",
                ConfirmPassword = "SuperSecretPassword123!"
            };
            var controller = new UsersController(PrepUserServiceWithoutUser());

            // Act
            var response = await controller.Register(registration);

            // Assert
            response.Should().BeOfType<OkResult>();
        }
        
        [Fact]
        public async Task Register_IncompleteRegistration_ReturnsBadRequest()
        {
            // Arrange
            var registration = Helpers.GetRegistration();
            registration.FirstName = null;
            registration.ConfirmPassword = null;
            var controller = PrepControllerWithoutUser();

            // Act
            var response = await controller.Register(registration);

            // Assert
            response.Should().BeOfType<BadRequestResult>();
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
            response.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public async Task ChangeEmail_ChangeSuccessful_ReturnsNoContent()
        {
            // Arrange
            var emailUpdate = Helpers.GetEmailUpdate();
            var controller = PrepUserServiceWithUser();

            // Act
            var response = await controller.ChangeEmail(emailUpdate);

            // Assert
            response.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public async Task ChangeEmail_IncompleteAccountUpdate_ReturnsBadRequest()
        {
            // Arrange
            var accountUpdate = Helpers.GetEmailUpdate();
            accountUpdate.EmailUpdate!.Email = null;
            var controller = PrepControllerWithUser();

            // Act
            var response = await controller.ChangeEmail(accountUpdate);

            // Assert
            response.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public async Task ChangePassword_ChangeSuccessful_ReturnsCreated()
        {
            // Arrange
            var accountUpdate = Helpers.GetPasswordUpdate();
            var controller = PrepControllerWithUser();

            // Act
            var response = await controller.ChangePassword(accountUpdate);

            // Assert
            response.Should().BeOfType<OkResult>();
        }
        
        [Fact]
        public async Task ChangePassword_IncompleteAccountUpdate_ReturnsBadRequest()
        {
            // Arrange
            var accountUpdate = Helpers.GetPasswordUpdate();
            accountUpdate.PasswordUpdate!.Password = "Goobledyblech";
            var controller = PrepControllerWithUser();

            // Act
            var response = await controller.ChangePassword(accountUpdate);

            // Assert
            response.Should().BeOfType<BadRequestResult>();
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
            response.Should().BeOfType<BadRequestResult>();
        }

        private IUserService PrepUserServiceWithoutUser()
        {
            var userService = GetMockUserService();
            userService.Setup(us => us.CreateAccount(It.IsAny<Registration>()))
                .ReturnsAsync(() => IdentityResult.Success);
            
            return userService.Object;
        }
        
        private IUserService PrepUserServiceWithUser()
        {
            var userService = GetMockUserService();
            userService.Setup(us => us.CreateAccount(It.IsAny<Registration>()))
                .Throws<DuplicateUserException>();
            userService.Setup(us => us.DeleteAccount("UserId"))
                .ReturnsAsync(() => IdentityResult.Success);
            userService.Setup(us => us.DeleteAccount("UserIdDelete"))
                .ReturnsAsync(() => IdentityResult.Failed());
            
            return userService.Object;
        }

        private UsersController PrepControllerWithoutUser()
            => new (PrepUserServiceWithoutUser());

        private UsersController PrepControllerWithUser()
            => new(PrepUserServiceWithUser());

        private Mock<IUserService> GetMockUserService() => new ();
    }
}