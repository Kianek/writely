using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Writely.Data;
using Writely.Exceptions;
using Writely.Models;

namespace Writely.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateAccount(Registration registration)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }
            if (!registration.IsComplete())
            {
                throw new IncompleteRegistrationException();
            }
            if (registration.Password != registration.ConfirmPassword)
            {
                throw new PasswordMismatchException("Passwords must match");
            }
            
            var user = await _userManager.FindByEmailAsync(registration.Email);
            if (user != null)
            {
                throw new DuplicateUserException($"That email is already registered: {registration.Email}");
            }
            
            var newUser = new AppUser(registration);
            return await _userManager.CreateAsync(newUser, registration.Password);
        }

        public async Task<IdentityResult> ChangeEmail(AccountUpdate update)
        {
            var user = await _userManager.FindByIdAsync(update.UserId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            
            var email = update.EmailUpdate?.Email;
            if (user.Email == email)
            {
                return IdentityResult.Success;
            }
            
            var emailChangeToken = await _userManager.GenerateChangeEmailTokenAsync(user, email);
            return await _userManager.ChangeEmailAsync(user, email, emailChangeToken);
        }

        public async Task<IdentityResult> ChangePassword(AccountUpdate update)
        {
            if (!update.PasswordUpdate.PasswordsMatch())
            {
                throw new PasswordMismatchException();
            }
            
            var user = await _userManager.FindByIdAsync(update.UserId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            
            var (oldPassword, newPassword) = update.PasswordUpdate!;
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public Task<IdentityResult> DeleteAccount(string userId)
        {
            throw new NotImplementedException();
        }
    }
}