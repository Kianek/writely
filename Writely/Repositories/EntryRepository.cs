using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Writely.Data;
using Writely.Exceptions;
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
            if (tags.Length == 0)
            {
                throw new EmptyTagsException("No tags provided");
            }
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

        public async Task<Entry> Create(Entry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException();
            }
            
            var journal = await _context.Journals
                .FindAsync(entry.JournalId);
            if (journal == null)
            {
                throw new JournalNotFoundException($"Journal not found: {entry.JournalId}");
            }
            
            journal.Entries.Add(entry);
            _context.Journals.Update(journal);
            await _context.SaveChangesAsync();
            return entry;
        }

        public async Task<Entry> Update(Entry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException();
            }
            
            _context.Entries.Update(entry);
            await _context.SaveChangesAsync();
            return entry;
        }

        public async Task<bool> Delete(long journalId, long entryId)
        {
            var journal = await _context.Journals.FindAsync(journalId);
            if (journal == null)
            {
                throw new JournalNotFoundException($"Journal not found: {journalId}");
            }
            
            var entry = journal.Entries.Find(e => e.Id == entryId);
            if (entry == null)
            {
                throw new EntryNotFoundException($"Entry not found: {entryId}");
            }
            
            journal.Entries.Remove(entry);
            _context.Journals.Update(journal);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}