using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Writely.Data;
using Writely.Exceptions;
using Writely.Models;

namespace Writely.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<AppUser> userManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IdentityResult> CreateAccount(Registration registration)
        {
            if (registration == null)
            {
                _logger.LogWarning("Registration object null");
                throw new ArgumentNullException(nameof(registration));
            }
            if (!registration.IsComplete())
            {
                _logger.LogWarning("Registration incomplete");
                throw new MissingInformationException();
            }
            if (registration.Password != registration.ConfirmPassword)
            {
                _logger.LogWarning("Passwords do not match");
                throw new PasswordMismatchException("Passwords must match");
            }
            
            _logger.LogInformation("Attempting to load user");
            var user = await _userManager.FindByEmailAsync(registration.Email);
            if (user != null)
            {
                _logger.LogWarning("Email already registered: {Email}", registration.Email);
                throw new DuplicateUserException($"That email is already registered: {registration.Email}");
            }
            
            _logger.LogInformation("Registering user");
            var newUser = new AppUser(registration);
            return await _userManager.CreateAsync(newUser, registration.Password);
        }

        public async Task<IdentityResult> ChangeEmail(AccountUpdate update)
        {
            if (update == null)
            {
                _logger.LogWarning("AccountUpdate object null");
                throw new ArgumentNullException(nameof(update));
            }
            if (update.EmailUpdate == null || string.IsNullOrEmpty(update.EmailUpdate?.Email))
            {
                _logger.LogWarning("EmailUpdate or Email null");
                throw new MissingInformationException("Missing email update");
            }
            
            _logger.LogInformation("Attempting to load user");
            var user = await _userManager.FindByIdAsync(update.UserId);
            if (user == null)
            {
                _logger.LogWarning("Unable to locate user: {Id}", update.UserId);
                throw new UserNotFoundException($"Unable to locate user: {update.UserId}");
            }
            
            var email = update.EmailUpdate?.Email;
            if (user.Email == email)
            {
                _logger.LogWarning("New email same as the old; no change made");
                return IdentityResult.Failed();
            }
            
            _logger.LogInformation("Changing email");
            var emailChangeToken = await _userManager.GenerateChangeEmailTokenAsync(user, email);
            return await _userManager.ChangeEmailAsync(user, email, emailChangeToken);
        }

        public async Task<IdentityResult> ChangePassword(AccountUpdate update)
        {
            if (update == null)
            {
                _logger.LogWarning("AccountUpdate is null");
                throw new ArgumentNullException(nameof(update));
            }

            var pw1 = update.PasswordUpdate?.CurrentPassword;
            var pw2 = update.PasswordUpdate?.NewPassword;
            if (update.PasswordUpdate == null
            || string.IsNullOrEmpty(pw1) || string.IsNullOrEmpty(pw2))
            {
                _logger.LogWarning("One or more passwords missing");
                throw new MissingInformationException("Missing password update");
            }
            
            _logger.LogInformation("Attempting to load user");
            var user = await _userManager.FindByIdAsync(update.UserId);
            if (user == null)
            {
                _logger.LogWarning("Unable to locate user: {Id}", update.UserId);
                throw new UserNotFoundException($"Unable to locate user: {update.UserId}");
            }
            
            _logger.LogInformation("Changing password");
            var (oldPassword, newPassword) = update.PasswordUpdate!;
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> DeleteAccount(string userId)
        {
            _logger.LogInformation("Attempting to load user");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("Unable to locate user: {Id}", userId);
                throw new UserNotFoundException($"Unable to locate user: {userId}");
            }
            
            
            _logger.LogInformation("Deleting user");
            return await _userManager.DeleteAsync(user);
        }
    }
}