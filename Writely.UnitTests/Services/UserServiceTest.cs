using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Writely.Data;
using Writely.Exceptions;
using Writely.Models;
using Writely.Services;
using Xunit;

namespace Writely.UnitTests.Services
{
    public class UserServiceTest
    {
        private Mock<UserManager<AppUser>> _manager;
        
        public UserServiceTest()
        {
            _manager = new Mock<UserManager<AppUser>>(
                new Mock<IUserStore<AppUser>>().Object, 
                null, 
                null,
                null, 
                null, 
                null,
                null, 
                null, 
                null
                );
        }
        
        [Fact]
        public async Task CreateAccount_UserNotRegistered_AccountCreated()
        {
            // Arrange
            var service = GetServiceWithoutUser();
            var registration = GetRegistrationModel();

            // Act
            var result = await service.CreateAccount(registration);

            // Assert
            result.Succeeded.Should().BeTrue();
        }
        
        
        [Fact]
        public async Task CreateAccount_AccountExists_ThrowsDuplicateUserException()
        {
            // Arrange
            var service = GetServiceWithUser();
            var registration = GetRegistrationModel();

            // Assert
            await service.Invoking(s => s.CreateAccount(registration))
                .Should()
                .ThrowAsync<DuplicateUserException>();
        }
        
        
        [Fact]
        public async Task CreateAccount_IncompleteRegistrationInfo_ThrowsIncompleteRegistrationException()
        {
            // Arrange
            var service = GetServiceWithoutUser();
            var registration = GetRegistrationModel();
            registration.Email = null;
            registration.ConfirmPassword = null;

            // Assert
            await service.Invoking(s => s.CreateAccount(registration))
                .Should()
                .ThrowAsync<IncompleteRegistrationException>();
        }
        
        [Fact]
        public async Task CreateAccount_PasswordMismatch_ThrowsPasswordMismatchException()
        {
            // Arrange
            var service = GetServiceWithoutUser();
            var registration = GetRegistrationModel();
            registration.ConfirmPassword = "TotallyMismatchingPW123!";

            // Assert
            await service.Invoking(s => s.CreateAccount(registration))
                .Should()
                .ThrowAsync<PasswordMismatchException>();
        }
        
        
        [Fact]
        public async Task CreateAccount_RegistrationModelNull_ThrowsArgumentNullException()
        {
            // Arrange
            var service = GetServiceWithUser();

            // Assert
            await service.Invoking(s => s.CreateAccount(null!))
                .Should()
                .ThrowAsync<ArgumentNullException>();
        }
        
        
        [Fact]
        public async Task ChangeEmail_UserFound_NewEmailValid_EmailChanged()
        {
            // Arrange
            var service = GetServiceWithUser();
            var update = GetEmailUpdateModel();

            // Act
            var result = await service.ChangeEmail(update);

            // Assert
            result.Succeeded.Should().BeTrue();
        }
        
        [Fact]
        public async Task ChangeEmail_UserFound_NewEmailSameAsOld_NoChangesMade()
        {
            // Arrange
            var service = GetServiceWithUser();
            var update = GetEmailUpdateModel("bob@gmail.com");
            
            // Act
            var result = await service.ChangeEmail(update);

            // Assert
            result.Succeeded.Should().BeFalse();
        }
        
        [Fact]
        public async Task ChangeEmail_UserNotFound_ThrowsUserNotFoundException()
        {
            // Arrange
            var service = GetServiceWithoutUser();
            var update = GetEmailUpdateModel();

            // Assert
            await service.Invoking(s => s.ChangeEmail(update))
                .Should()
                .ThrowAsync<UserNotFoundException>();
        }
        
        [Fact]
        public async Task ChangePassword_UserFound_ConfirmationPasswordMatches_PasswordChanged()
        {
            // Arrange
            var service = GetServiceWithUser();
            var update = GetPasswordUpdateModel();
            
            // Act
            var result = await service.ChangePassword(update);

            // Assert
            result.Succeeded.Should().BeTrue();

        }
        
        [Fact]
        public async Task ChangePassword_UserFound_ConfirmationPasswordMismatch_ThrowsPasswordMismatchException()
        {
            // Arrange
            var service = GetServiceWithUser();
            var update = GetPasswordUpdateModel();
            update.PasswordUpdate!.ConfirmPassword = "ATotallyDifferentPW123!";

            // Assert
            await service.Invoking(s => s.ChangePassword(update))
                .Should()
                .ThrowAsync<PasswordMismatchException>();
        }
        
        [Fact]
        public async Task ChangePassword_UserNotFound_ThrowsUserNotFoundException()
        {
            // Arrange
            var service = GetServiceWithoutUser();
            var update = GetPasswordUpdateModel();

            // Assert
            await service.Invoking(s => s.ChangePassword(update))
                .Should()
                .ThrowAsync<UserNotFoundException>();
        }
        
        [Fact]
        public async Task DeleteAccount_UserFound_AccountDeleted()
        {
            // Arrange
            var service = GetServiceWithUser();

            // Act
            var result = await service.DeleteAccount("UserId");

            // Assert
            result.Succeeded.Should().BeTrue();
        }
        
        
        [Fact]
        public async Task DeleteAccount_UserNotFound_ThrowsUserNotFoundException()
        {
            // Arrange
            var service = GetServiceWithoutUser();

            // Assert
            await service.Invoking(s => s.DeleteAccount("UserId"))
                .Should()
                .ThrowAsync<UserNotFoundException>();
        }

        private IUserService GetUserService(UserManager<AppUser> userManager) => new UserService(userManager);

        private Registration GetRegistrationModel() => new Registration
        {
            Username = "bob.loblaw",
            FirstName = "Bob",
            LastName = "Loblaw",
            Email = "bob@loblawlaw.com",
            Password = "SecretPassword123!",
            ConfirmPassword = "SecretPassword123!",
        };

        private IUserService GetServiceWithUser(string email = "bob@gmail.com") => 
            GetUserService(PrepUserManagerWithUser(email));
        
        private IUserService GetServiceWithoutUser(string email = "bob@gmail.com") =>
            GetUserService(PrepUserManagerWithoutUser(email));

        private AccountUpdate GetEmailUpdateModel(string email = "spiffynewemail@gmail.com")
            => new("UserId", emailUpdate: new() { Email = email });

        private AccountUpdate GetPasswordUpdateModel(string password = "SpiffierPassword123!!")
            => new("UserId", passwordUpdate: 
                new(){Password = password, ConfirmPassword = password});

        private UserManager<AppUser> PrepUserManagerWithUser(string email = "bob@gmail.com")
        {
            _manager.Setup(um => um.FindByEmailAsync(email))
                .ReturnsAsync(() => new AppUser());
            return _manager.Object;
        }

        private UserManager<AppUser> PrepUserManagerWithoutUser(string email = "bob@gmail.com")
        {
            _manager.Setup(um => um.FindByEmailAsync(email))
                .ReturnsAsync(() => null!);
            return _manager.Object;
        }
    }
}