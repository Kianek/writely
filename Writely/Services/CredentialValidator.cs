using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Writely.Data;
using Writely.Models;

namespace Writely.Services
{
    public class CredentialValidator : ICredentialValidator
    {
        private readonly UserManager<AppUser> _userManager;

        public CredentialValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> AreValidCredentials(Credentials credentials)
        {
            var user = await _userManager.FindByEmailAsync(credentials.Email);
            return await _userManager.CheckPasswordAsync(user, credentials.Password);
        }
    }
}