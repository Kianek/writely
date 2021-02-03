using Writely.Models;
using Writely.Repositories;

namespace Writely.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _userId;
        private readonly long _journalId;
        private readonly AppDbContext _context;
        private IRepository<Journal>? _journals;
        private IEntryRepository? _entries;

        public IRepository<Journal> Journals => 
            _journals ?? new JournalRepository(_context, _userId);

        public IEntryRepository Entries =>
            _entries ?? new EntryRepository(_context, _journalId);

        public UnitOfWork(AppDbContext context, string userId, long journalId = 0L)
        {
            _context = context;
            _userId = userId;
            _journalId = journalId;
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