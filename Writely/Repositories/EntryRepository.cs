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

        public Task<Entry> GetById(string userId, long journalId, long entryId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Entry>> GetAllByJournal(string userId, long journalId, string order = "date-desc")
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Entry>> GetAllByTag(string userId, string[] tags, string order = "date-desc")
        {
            throw new System.NotImplementedException();
        }

        public Task<Entry> Save(Entry entry)
        {
            throw new System.NotImplementedException();
        }

        public Task<Entry> Update(Entry entry)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(long journalId, long entryId)
        {
            throw new System.NotImplementedException();
        }
    }
}