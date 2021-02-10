using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Data;
using Writely.Models;

namespace Writely.Services
{
    public class EntryService : IEntryService
    {
        private AppDbContext _context;
        
        public long JournalId { get; set; }

        public EntryService(AppDbContext context)
        {
            _context = context;
        }

        public Task<Entry> GetById(long entryId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Entry>> GetAllByJournal(long journalId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Entry>> GetAllByTag(long journalId, string tags, string order = "date-desc")
        {
            throw new System.NotImplementedException();
        }

        public Task<Entry> Add(NewEntryModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<Entry> Update(long entryId, EntryUpdateModel updateModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<Entry> Remove(long entryId)
        {
            throw new System.NotImplementedException();
        }
    }
}