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

        public Task<IdentityResult> ChangeEmail(AccountUpdate update)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> ChangePassword(AccountUpdate update)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAccount(string userId)
        {
            throw new NotImplementedException();
        }
    }
}