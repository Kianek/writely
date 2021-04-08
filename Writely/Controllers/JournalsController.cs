using System;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Writely.Exceptions;
using Writely.Extensions;
using Writely.Models;
using Writely.Models.Dto;
using Writely.Services;

namespace Writely.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalsController : ControllerBase
    {
        private readonly ILogger<JournalsController> _logger;
        private readonly IJournalService _journalService;

        public JournalsController(ILogger<JournalsController> logger, IJournalService journalService)
        {
            _logger = logger;
            _journalService = journalService;
            _journalService.UserId ??= HttpContext.User.Identity.GetSubjectId();
        }

        [HttpGet("journalId")]
        public async Task<IActionResult> GetById(long journalId)
        {
            JournalDto journal;
            try
            {
                journal = (await _journalService.GetById(journalId)).ToDto();
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (JournalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok(journal);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewJournal newJournal)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{journalId}")]
        public async Task<IActionResult> Update(long journalId, JournalUpdate update)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{journalId}")]
        public async Task<IActionResult> Remove(long journalId)
        {
            throw new NotImplementedException();
        }
    }
}