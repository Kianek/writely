using Writely.Models;
using Writely.Repositories;

namespace Writely.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;

        public IRepository<Journal> Journals { get; private set; }
        public IEntryRepository Entries { get; private set;  }

        public UnitOfWork(AppDbContext context, string userId, long journalId)
        {
            _context = context;
            Journals = new JournalRepository(_context, userId);
            Entries = new EntryRepository(_context, journalId);
        }
        
        public int Complete()
        {
            throw new System.NotImplementedException();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}