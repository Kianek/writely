using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Writely.Data;
using Writely.Models;

namespace Writely.Services
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAccount(Registration registration);
        Task<IdentityResult> ChangeEmail(EmailUpdate update);
        Task<IdentityResult> ChangePassword(PasswordUpdate update);
        Task<IdentityResult> DeleteAccount(string userId);
    }
}