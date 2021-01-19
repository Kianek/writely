using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Data;
using Writely.Models;

namespace Writely.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly AppDbContext _context;

        public EntryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Entry> GetById(long entryId)
        {
            return await _context.Entries.FindAsync(entryId);
        }

        public async Task<List<Entry>> GetAllByJournal(long journalId, string order = "date-desc")
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Entry>> GetAllByTag(long journalId, string[] tags, string order = "date-desc")
        {
            throw new System.NotImplementedException();
        }

        public async Task<Entry> Save(Entry entry)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Entry> Update(Entry entry)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Delete(long journalId, long entryId)
        {
            throw new System.NotImplementedException();
        }
    }
}