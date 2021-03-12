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
        private readonly IUserService _service;
        
        public UsersControllerTests()
        {
        }

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
            // var registration = 
            
            // Act
            
            // Assert
        }
        
        [Fact]
        public async Task Register_UserAlreadyRegistered_ReturnsBadRequest()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
        
        [Fact]
        public async Task ChangeEmail_ChangeSuccessful_ReturnsCreated()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
        [Fact]
        
        public async Task ChangeEmail_IncompleteAccountUpdate_ReturnsBadRequest()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
        
        [Fact]
        public async Task ChangePassword_ChangeSuccessful_ReturnsCreated()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
        
        [Fact]
        public async Task ChangePassword_IncompleteAccountUpdate_ReturnsBadRequest()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
        [Fact]
        
        public async Task DeleteAccount_AccountDeleted_ReturnsOk()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
        
        [Fact]
        public async Task DeleteAccount_AccountNotFound_ReturnsBadRequest()
        {
            // Arrange
            
            // Act
            
            // Assert
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
            
            return userService.Object;
        }

        private UsersController PrepControllerWithoutUser()
            => new (PrepUserServiceWithoutUser());
        

        private Mock<IUserService> GetMockUserService() => new ();
    }
}