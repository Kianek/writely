using System;
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

        public async Task<Journal> GetById(long id)
        {
            return await _context.Journals
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<List<Journal>> GetAllByUserId(string userId, int limit = 0, string order = "date-desc")
        {
            var query = _context.Journals
                .Where(j => j.UserId == userId)
                .AsNoTracking()
                .SortBy(order);
            
            if (limit <= 0)
            {
                return await query.ToListAsync();
            }

            return await query.Take(limit).ToListAsync();
        }

        public async Task<Journal> Save(Journal journal)
        {
            if (journal == null)
            {
                throw new ArgumentNullException();
            }
            
            _context.Journals?.Add(journal);
            await _context.SaveChangesAsync();
            return journal;
        }

        public async Task<Journal> Update(Journal journal)
        {
            if (journal == null)
            {
                throw new ArgumentNullException();
            }
            
            _context.Journals?.Update(journal);
            await _context.SaveChangesAsync();
            return journal;
        }

        public async Task<bool> Delete(long id)
        {
            var journal = await _context.Journals
                .Where(j => j.Id == id)
                    .Include(j => j.Entries)
                .FirstOrDefaultAsync();
            if (journal == null)
            {
                return false;
            }
            
            _context.Journals.Remove(journal);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}