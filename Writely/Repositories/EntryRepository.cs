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
        private readonly long _journalId;

        public EntryRepository(AppDbContext context, long journalId) : base(context)
        {
            _journalId = journalId;
        }

        public override async Task<IEnumerable<Entry>?> GetAll(
            Expression<Func<Entry, bool>>? filter = null, string? order = null, int limit = 0)
        {
            var query = _context.Entries?.Where(e => e.JournalId == _journalId);
            if (filter != null)
            {
                query = query!.Where(filter);
            }

            if (order != null)
            {
                query = query!.SortBy(order);
            }

            if (limit > 0)
            {
                query = query!.Take(limit);
            }
            
            return await query!.ToListAsync();
        }

        public async Task<IEnumerable<Entry>?> GetAllByTag(string[] tags, string? order = "date-desc")
        {
            if (tags.Length <= 0)
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

            var query = journal.Entries.GetByTag(tags);
            if (order != null)
            {
                query = query.SortBy(order);
            }
            return query.ToList();
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