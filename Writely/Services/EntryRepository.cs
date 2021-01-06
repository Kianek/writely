using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Data;
using Writely.Models;

namespace Writely.Services
{
    public class EntryRepository : IEntryRepository
    {
        private readonly AppDbContext _context;

        public EntryRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Entry> GetById(long entryId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Entry>> GetAllByJournal(long journalId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Entry> Save(NewEntryModel entry)
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