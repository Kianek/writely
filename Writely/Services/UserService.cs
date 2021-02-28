using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Writely.Data;
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

        public Task<IdentityResult> CreateAccount(UserRegistrationModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> ChangeEmail(UserEmailUpdateModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> ChangePassword(UserPasswordUpdateModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAccount(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}