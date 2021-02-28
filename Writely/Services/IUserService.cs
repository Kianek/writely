using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Writely.Data;
using Writely.Models;

namespace Writely.Services
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAccount(UserRegistrationModel model);
        Task<IdentityResult> ChangeEmail(UserEmailUpdateModel model);
        Task<IdentityResult> ChangePassword(UserPasswordUpdateModel model);
        Task<IdentityResult> DeleteAccount(string userId);
    }
}