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

        [HttpGet("{journalId:long}")]
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

        [HttpGet("{journalId:long}/entries")]
        public async Task<IActionResult> GetAllJournalEntries(long journalId, [FromQuery] QueryFilter filter)
        {
            PrepJournalServiceWithUserId();
            IEnumerable<EntryDto>? entries;
            try
            {
                entries = (await _journalService.GetAllEntries(journalId, filter))
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

        [HttpPatch("{journalId:long}")]
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

        [HttpDelete("{journalId:long}")]
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
        
        // Entry methods
        [HttpGet("{journalId:long}/entries/{entryId:long}")]
        public async Task<IActionResult> GetEntry(long journalId, long entryId)
        {
            Entry entry;
            try
            {
                entry = await _journalService.GetEntry(journalId, entryId);
            }
            catch (JournalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EntryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return Ok(entry);
        }
        
        [HttpPost("{journalId:long}/entries")]
        public async Task<IActionResult> AddEntry(long journalId, [FromBody] NewEntry newEntry)
        {
            Entry entry;
            
            try
            {
                entry = await _journalService.AddEntry(journalId, newEntry);
            }
            catch (JournalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return CreatedAtAction(nameof(AddEntry), entry);
        }
        
        [HttpPatch("{journalId:long}/entries/{entryId:long}")]
        public async Task<IActionResult> UpdateEntry(long journalId, long entryId, [FromBody] EntryUpdate update)
        {
            Entry entry;

            try
            {
                entry = await _journalService.UpdateEntry(journalId, entryId, update);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (JournalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EntryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return NoContent();
        }
        
        [HttpDelete("{journalId:long}/entries/{entryId:long}")]
        public async Task<IActionResult> DeleteEntry(long journalId, long entryId)
        {
            try
            {
                await _journalService.RemoveEntry(journalId, entryId);
            }
            catch (JournalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EntryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return Ok();
        }

        private void PrepJournalServiceWithUserId() => _journalService.UserId = HttpContext?.User.GetSubjectId();
    }
}