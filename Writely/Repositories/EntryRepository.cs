using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Entry>?> GetAllByJournal(long journalId, string order = "date-desc")
        {
            var journal = await _context.Journals.FindAsync(journalId);
            return journal?.Entries;
        }

        public async Task<List<Entry>?> GetAllByTag(long journalId, string[] tags, string order = "date-desc")
        {
            var journal = await _context.Journals
                .AsNoTracking()
                .Where(j => j.Id == journalId)
                    .Include(j => j.Entries)
                .FirstOrDefaultAsync();
            var entries = journal?.Entries
                .Where(entry =>
                {
                    return tags.All(t => entry.Tags.Contains(t));
                })
                .ToList();
            return entries;
        }

        public async Task<Entry> Save(Entry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException();
            }
            
            var journal = await _context.Journals
                .FindAsync(entry.JournalId);
            if (journal == null)
            {
                return null;
            }
            
            journal.Entries.Add(entry);
            _context.Journals.Update(journal);
            await _context.SaveChangesAsync();
            return entry;
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