using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Writely.Exceptions;
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
            try
            {
                await _userService.CreateAccount(registration);
            }
            catch (MissingInformationException ex)
            {
                return BadRequest(ex);
            }
            catch (DuplicateUserException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
            return Ok();
        }

        [HttpPatch("change-email")]
        public async Task<IActionResult> ChangeEmail(AccountUpdate update)
        {
            IdentityResult result;
            
            try
            {
                result = await _userService.ChangeEmail(update);
            }
            catch (MissingInformationException ex)
            {
                return BadRequest(ex);
            }

            if (!result.Succeeded)
            {
                return BadRequest();
            }
            
            return Ok();
        }
        
        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword(AccountUpdate update)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{userId:string}")]
        public async Task<IActionResult> DeleteAccount(string userId)
        {
            throw new NotImplementedException();
        }
    }
}