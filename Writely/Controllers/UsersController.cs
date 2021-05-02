using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Writely.Exceptions;
using Writely.Models;
using Writely.Services;

namespace Writely.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(Registration registration)
        {
            try
            {
                await _userService.CreateAccount(registration);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result.Succeeded)
            {
                return BadRequest();
            }
            
            return NoContent();
        }
        
        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword(AccountUpdate update)
        {
            try
            {
                await _userService.ChangePassword(update);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAccount(string userId)
        {
            try
            {
                await _userService.DeleteAccount(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}