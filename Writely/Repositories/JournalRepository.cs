using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Writely.Data;
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

        public Task<List<Journal>> GetAll(string userId, int limit = 0, string orderBy = "date-desc")
        {
            throw new System.NotImplementedException();
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