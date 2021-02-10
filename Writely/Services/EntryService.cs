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

        public EntryService(AppDbContext context, long? journalId)
        {
            _context = context;
            JournalId = journalId ?? 0L;
        }

        public Task<Entry> GetById(long entryId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Entry>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Entry>> GetAllByTag(string tags, string order = "date-desc")
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