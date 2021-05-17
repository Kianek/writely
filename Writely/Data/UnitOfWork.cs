using System;
using System.Threading.Tasks;
using Writely.Models;
using Writely.Repositories;

namespace Writely.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        public string? UserId { get; set; }
        public long? JournalId { get; set; }
        private readonly AppDbContext _context;
        private IRepository<Journal>? _journals;
        private IEntryRepository? _entries;

        public IRepository<Journal> Journals => 
            _journals ??= new JournalRepository(_context, UserId!);

        public IEntryRepository Entries =>
            _entries ??= new EntryRepository(_context, JournalId);

        public UnitOfWork(AppDbContext context) => _context = context;
        
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}