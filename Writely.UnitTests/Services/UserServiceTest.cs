using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Writely.Data;
using Writely.Exceptions;
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
            var registration = Helpers.GetRegistration();

            // Act
            var result = await service.CreateAccount(registration);

            // Assert
            result.Succeeded.Should().BeTrue();
        }
        
        
        [Fact]
        public async Task CreateAccount_AccountExists_ThrowsDuplicateUserException()
        {
            // Arrange
            _manager.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new AppUser());
            var service = GetServiceWithUser();
            var registration = Helpers.GetRegistration();

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
            var registration = Helpers.GetRegistration();
            registration.Email = null;
            registration.ConfirmPassword = null;

            // Assert
            await service.Invoking(s => s.CreateAccount(registration))
                .Should()
                .ThrowAsync<MissingInformationException>();
        }
        
        [Fact]
        public async Task CreateAccount_PasswordMismatch_ThrowsPasswordMismatchException()
        {
            // Arrange
            var service = GetServiceWithoutUser();
            var registration = Helpers.GetRegistration();
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
            var update = Helpers.GetEmailUpdate();
            _manager.Setup(um
                    => um.GenerateChangeEmailTokenAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(() => "niftyEmailToken");
            _manager.Setup(um => um.ChangeEmailAsync(It.IsAny<AppUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => IdentityResult.Success);

            // Act
            var result = await service.ChangeEmail(update);

            // Assert
            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task ChangeEmail_EmailUpdateMissing_ThrowsMissingInformationException()
        {
            // Arrange
            var service = GetServiceWithUser();
            var update = Helpers.GetEmailUpdate();
            update.EmailUpdate = null;

            // Assert
            await service.Invoking(us => us.ChangeEmail(update))
                .Should()
                .ThrowAsync<MissingInformationException>();
        }
        
        [Fact]
        public async Task ChangeEmail_UserFound_NewEmailSameAsOld_NoChangesMade_ReturnsSuccess()
        {
            // Arrange
            var email = "bob@gmail.com";
            var service = GetServiceWithUser(email);
            var update = Helpers.GetEmailUpdate(email);
            
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
            var update = Helpers.GetEmailUpdate();

            // Assert
            await service.Invoking(s => s.ChangeEmail(update))
                .Should()
                .ThrowAsync<UserNotFoundException>();
        }

        [Fact]
        public async Task ChangeEmail_AccountUpdateNull_ThrowsArgumentNullException()
        {
            // Arrange
            var service = GetServiceWithUser();

            // Assert
            await service.Invoking(us => us.ChangeEmail(null!))
                .Should()
                .ThrowAsync<ArgumentNullException>();
        }
        
        [Fact]
        public async Task ChangePassword_UserFound_ConfirmationPasswordMatches_PasswordChanged()
        {
            // Arrange
            var service = GetServiceWithUser();
            var update = Helpers.GetPasswordUpdate();
            
            // Act
            var result = await service.ChangePassword(update);

            // Assert
            result.Succeeded.Should().BeTrue();

        }

        [Fact]
        public async Task ChangePassword_AccountUpdateNull_ThrowsArgumentNullException()
        {
            // Arrange
            var service = GetServiceWithUser();

            // Assert
            await service.Invoking(us => us.ChangePassword(null!))
                .Should()
                .ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task ChangePassword_PasswordUpdateMissing_ThrowsMissingInformationException()
        {
            // Arrange
            var service = GetServiceWithUser();
            var update = Helpers.GetPasswordUpdate();
            update.PasswordUpdate = null;

            // Assert
            await service.Invoking(us => us.ChangePassword(update))
                .Should()
                .ThrowAsync<MissingInformationException>();
        }
        
        [Fact]
        public async Task ChangePassword_UserFound_ConfirmationPasswordMismatch_ThrowsPasswordMismatchException()
        {
            // Arrange
            var service = GetServiceWithUser();
            var update = Helpers.GetPasswordUpdate();
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
            var update = Helpers.GetPasswordUpdate();

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

        private IUserService GetServiceWithUser(string email = "bob@gmail.com") => 
            GetUserService(PrepUserManagerWithUser(email));
        
        private IUserService GetServiceWithoutUser(string email = "bob@gmail.com") =>
            GetUserService(PrepUserManagerWithoutUser(email));

        private UserManager<AppUser> PrepUserManagerWithUser(string email = "bob@gmail.com")
        {
            _manager.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(() => IdentityResult.Failed());
            _manager.Setup(um => um.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new AppUser() { Email = email});
            _manager.Setup(um => um.FindByEmailAsync(email))
                .ReturnsAsync(() => new AppUser());
            _manager.Setup(um
                    => um.ChangePasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => IdentityResult.Success);
            _manager.Setup(um => um.DeleteAsync(It.IsAny<AppUser>()))
                .ReturnsAsync(() => IdentityResult.Success);
            return _manager.Object;
        }

        private UserManager<AppUser> PrepUserManagerWithoutUser(string email = "bob@gmail.com")
        {
            _manager.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(() => IdentityResult.Success);
            _manager.Setup(um => um.FindByEmailAsync(email))
                .ReturnsAsync(() => null!);
            _manager.Setup(um
                    => um.ChangePasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => IdentityResult.Failed());
            _manager.Setup(um => um.DeleteAsync(It.IsAny<AppUser>()))
                .ReturnsAsync(() => IdentityResult.Failed());
            return _manager.Object;
        }
    }
}