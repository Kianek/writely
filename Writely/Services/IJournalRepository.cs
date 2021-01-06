using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;

namespace Writely.Services
{
    public interface IJournalRepository
    {
        Task<Journal> GetById(string userId, long id);
        Task<List<Journal>> GetAll(string userId, string query = "", string order = "", int limit = 0);
        Task<Journal> Save(Journal journal);
        Task<Journal> Update(IModelUpdater<Journal, JournalUpdateModel> updater, 
            JournalUpdateModel model);
        Task Delete(long id);
    }
}