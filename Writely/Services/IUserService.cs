using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Writely.Data;
using Writely.Models;

namespace Writely.Services
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAccount(UserRegistrationModel model);
        Task ChangeEmail(UserEmailUpdateModel model);
        Task ChangePassword(UserPasswordUpdateModel model);
        Task DeleteAccount(string userId);
    }
}