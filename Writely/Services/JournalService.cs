using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Data;
using Writely.Models;
using Writely.Models.Dto;

namespace Writely.Services
{
    public class JournalService : IJournalService
    {
        private readonly AppDbContext _context;
        
        public string? UserId { get; set; }

        public JournalService(AppDbContext context)
        {
            _context = context;
        }

        public Task<Journal> GetById(long journalId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Journal>> GetAll(int limit = 0, string orderBy = "date-desc")
        {
            throw new System.NotImplementedException();
        }

        public Task<Journal> Add(NewJournalModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<Journal> Update(long journalId, JournalUpdateModel updateModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Remove(long journalId)
        {
            throw new System.NotImplementedException();
        }
    }
}