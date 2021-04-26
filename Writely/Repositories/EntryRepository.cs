using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Writely.Data;
using Writely.Exceptions;
using Writely.Extensions;
using Writely.Models;

namespace Writely.Repositories
{
    public class EntryRepository : BaseRepository<Entry>, IEntryRepository
    {
        private readonly long? _journalId;

        public EntryRepository(AppDbContext context, long? journalId) : base(context)
        {
            _journalId = journalId;
        }

        public override async Task<IEnumerable<Entry>?> GetAll(QueryFilter? filter = null)
        {
            var journal = await _context.Journals!.FindAsync(_journalId);
            if (journal == null)
            {
                throw new JournalNotFoundException($"Journal not found: {_journalId}");
            }
            
            if (filter == null)
            {
                return journal.Entries;
            }
            
            var entries = journal.Entries as IEnumerable<Entry>;
            if (!string.IsNullOrEmpty(filter.OrderBy))
            {
                entries = entries.SortBy(filter.OrderBy);
            }
            
            if (filter.Limit > 0)
            {
                entries = entries.Take(filter.Limit);
            }
            
            return entries.ToList();
        }

        public async Task<IEnumerable<Entry>?> GetAllByTag(QueryFilter? filter)
        {
            if (string.IsNullOrEmpty(filter?.Tags))
            {
                throw new EmptyTagsException("No tags to search by");
            }
            
            var journal = await _context.Journals!
                .AsNoTracking()
                .Where(j => j.Id == _journalId)
                    .Include(j => j.Entries)
                .FirstOrDefaultAsync();
            if (journal == null)
            {
                throw new JournalNotFoundException($"Journal not found: {_journalId}");
            }

            return journal.Entries
                .GetByTag(filter.Tags.Split(","))
                .SortBy(filter.OrderBy)
                .ToList();
        }

        public override async Task<Entry?> Find(Expression<Func<Entry, bool>> predicate)
        {
            return await _context.Entries!
                .AsNoTracking()
                .Where(e => e.JournalId == _journalId)
                .Where(predicate)
                .FirstOrDefaultAsync();
        }
    }
}