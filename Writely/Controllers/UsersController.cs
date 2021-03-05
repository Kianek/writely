using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Writely.Models;
using Writely.Services;

namespace Writely.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Registration registration)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("change-email")]
        public async Task<IActionResult> ChangePassword(EmailUpdate update)
        {
            throw new NotImplementedException();
        }
        
        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword(PasswordUpdate update)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAccount(string userId)
        {
            throw new NotImplementedException();
        }
    }
}