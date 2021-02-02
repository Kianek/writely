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
        
        public override async Task<IEnumerable<Journal>?> GetAll(Expression<Func<Journal, bool>>? filter = null, string? order = null, int limit = 0)
        {
            var query = _context.Journals.AsNoTracking().Where(j => j.UserId == _userId);
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (order != null)
            {
                query = query.SortBy(order);
            }

            if (limit > 0)
            {
                query = query.Take(limit);
            }

            return await query.ToListAsync();
        }

        public override async Task<Journal?> Find(Expression<Func<Journal, bool>> predicate)
        {
            return await _context.Journals
                .AsNoTracking()
                .Where(j => j.UserId == _userId)
                .FirstOrDefaultAsync(predicate);
        }
    }
}