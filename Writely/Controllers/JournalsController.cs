using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> GetAll([FromQuery] QueryFilter filter)
        {
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
    }
}