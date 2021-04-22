using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Writely.Data;
using Writely.Extensions;
using Writely.Models;

namespace Writely.Repositories
{
    public class JournalRepository : BaseRepository<Journal>
    {
        private readonly string _userId;

        public JournalRepository(AppDbContext context, string userId) : base(context)
        {
            _userId = userId;
        }
        
        public override async Task<IEnumerable<Journal>?> GetAll(QueryFilter? filter = null)
        {
            var query = _context.Journals!
                .AsNoTracking()
                .Where(j => j.UserId == _userId);
            
            if (filter != null)
            {
                query = query.SortBy(filter.OrderBy);
            }

            if (filter?.Limit > 0)
            {
                query = query.Take(filter.Limit);
            }

            return await query.Include(j => j.Entries).ToListAsync();
        }

        public override async Task<Journal?> Find(Expression<Func<Journal, bool>> predicate)
        {
            return await _context.Journals!
                .AsNoTracking()
                .Where(j => j.UserId == _userId)
                .Include(j => j.Entries)
                .FirstOrDefaultAsync(predicate);
        }
    }
}