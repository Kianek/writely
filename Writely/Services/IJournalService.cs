using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;
using Writely.Models.Dto;

namespace Writely.Services
{
    public interface IJournalService
    {
        Task<Journal> GetById(string userId, long journalId);
        Task<List<Journal>> GetAll(int limit = 0, string orderBy = "date-desc");
        Task<Journal> Add(NewJournalModel model);

        Task<Journal> Update(long journalId, JournalUpdateModel updateModel);

        Task<bool> Remove(long journalId);
    }
}