using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Writely.Data;
using Writely.Extensions;
using Writely.Models;
using Writely.Services;

namespace Writely.Repositories
{
    public class JournalRepository : IJournalRepository
    {
        private readonly AppDbContext _context;

        public JournalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Journal> GetById(string userId, long id)
        {
            return await _context.Journals
                .Where(j => j.UserId == userId)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<List<Journal>> GetAll(string userId, int limit = 0, string order = "date-desc")
        {
            var query = _context.Journals
                .Where(j => j.UserId == userId)
                .SortBy(order);
            
            if (limit <= 0)
            {
                return await query.ToListAsync();
            }

            return await query.Take(limit).ToListAsync();
        }

        public Task<Journal> Save(Journal journal)
        {
            throw new System.NotImplementedException();
        }

        public Task<Journal> Update(Journal journal)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}