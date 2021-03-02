using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Writely.Models;

namespace Writely.Services
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAccount(Registration registration);
        Task<IdentityResult> ChangeEmail(AccountUpdate update);
        Task<IdentityResult> ChangePassword(AccountUpdate update);
        Task<IdentityResult> DeleteAccount(string userId);
    }
}