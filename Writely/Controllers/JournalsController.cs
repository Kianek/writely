using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        }

        [HttpGet("journalId")]
        public async Task<IActionResult> GetById(long journalId)
        {
            throw new NotImplementedException();
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

        [HttpPatch]
        public async Task<IActionResult> Update(JournalUpdate update)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{journalId}")]
        public async Task<IActionResult> Delete(long journalId)
        {
            throw new NotImplementedException();
        }
    }
}