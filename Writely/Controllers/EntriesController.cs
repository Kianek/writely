using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Writely.Exceptions;
using Writely.Extensions;
using Writely.Models;
using Writely.Services;

namespace Writely.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly ILogger<EntriesController> _logger;
        private readonly IEntryService _entryService;

        public EntriesController(ILogger<EntriesController> logger, IEntryService entryService)
        {
            _logger = logger;
            _entryService = entryService;
        }

        [HttpGet("{entryId:long}")]
        public async Task<IActionResult> GetById(long entryId)
        {
            Entry entry;
            try
            {
                entry = await _entryService.GetById(entryId);
            }
            catch (EntryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return Ok(entry);
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewEntry newEntry)
        {
            Entry entry;
            // TODO: refactor this
            _entryService.JournalId = newEntry.JournalId;
            try
            {
                entry = await _entryService.Add(newEntry);
            }
            catch (JournalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return Created(nameof(Add), entry.ToDto());
        }

        [HttpPatch("{entryId:long}")]
        public async Task<IActionResult> Update(long entryId, EntryUpdate update)
        {
            try
            {
                await _entryService.Update(entryId, update);
            }
            catch (EntryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return NoContent();
        }

        [HttpDelete("{entryId:long}")]
        public async Task<IActionResult> Delete(long entryId)
        {
            Entry entry;
            try
            {
                entry = await _entryService.Remove(entryId);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            
            return Ok(entry);
        }
    }
}