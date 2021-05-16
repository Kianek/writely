using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Writely.Exceptions;
using Writely.Extensions;
using Writely.Models;
using Writely.Models.Dto;
using Writely.Services;

namespace Writely.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class JournalsController : ControllerBase
    {
        private readonly ILogger<JournalsController> _logger;
        private readonly IJournalService _journalService;

        public JournalsController(ILogger<JournalsController> logger, IJournalService journalService)
        {
            _logger = logger;
            _journalService = journalService;
        }

        [HttpGet("{journalId}")]
        public async Task<IActionResult> GetById(long journalId)
        {
            PrepJournalServiceWithUserId();
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
        public async Task<IActionResult> GetAll([FromQuery] QueryFilter filter)
        {
            PrepJournalServiceWithUserId();
            List<JournalDto>? journals;

            try
            {
                journals = (await _journalService.GetAll(filter))?.ToList().MapToDto();
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return Ok(journals);
        }

        [HttpGet("{journalId}/entries")]
        public async Task<IActionResult> GetEntriesByJournal(long journalId, [FromQuery] QueryFilter filter)
        {
            PrepJournalServiceWithUserId();
            IEnumerable<EntryDto>? entries;
            try
            {
                entries = (await _journalService.GetEntriesByJournal(journalId, filter))
                    ?.ToList().MapToDto();
            }
            catch (JournalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return Ok(entries);
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewJournal newJournal)
        {
            PrepJournalServiceWithUserId();
            JournalDto journal;
            try
            {
                journal = (await _journalService.Add(newJournal)).ToDto();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return CreatedAtAction(nameof(Add), journal);
        }

        [HttpPatch("{journalId}")]
        public async Task<IActionResult> Update(long journalId, JournalUpdate update)
        {
            PrepJournalServiceWithUserId();
            try
            {
                await _journalService.Update(journalId, update);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (JournalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return NoContent();
        }

        [HttpDelete("{journalId}")]
        public async Task<IActionResult> Remove(long journalId)
        {
            PrepJournalServiceWithUserId();
            try
            {
                await _journalService.Remove(journalId);
            }
            catch (JournalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return Ok();
        }

        private void PrepJournalServiceWithUserId() => _journalService.UserId = HttpContext?.User.GetSubjectId();
    }
}