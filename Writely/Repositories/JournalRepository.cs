using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task<Journal> GetById(string userId, long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Journal>> GetAll(string userId, int limit = 0, string orderBy = "date-desc")
        {
            throw new System.NotImplementedException();
        }

        public Task<Journal> Save(Journal journal)
        {
            throw new System.NotImplementedException();
        }

        public Task<Journal> Update(IModelUpdater<Journal, JournalUpdateModel> updater, JournalUpdateModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}