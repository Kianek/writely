using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;
using Writely.Services;

namespace Writely.Repositories
{
    public interface IJournalRepository
    {
        Task<Journal> GetById(string userId, long id);
        Task<List<Journal>> GetAll(string userId, int limit = 0, string orderBy = "date-desc");
        Task<Journal> Save(Journal journal);
        Task<Journal> Update(IModelUpdater<Journal, JournalUpdateModel> updater, 
            JournalUpdateModel model);
        Task Delete(long id);
    }
}