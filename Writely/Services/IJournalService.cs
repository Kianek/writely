using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;
using Writely.Models.Dto;

namespace Writely.Services
{
    public interface IJournalService
    {
        Task<JournalDto> GetById(string userId, long journalId);
        Task<List<JournalDto>> GetAllByUserId(string userId, int limit = 0, string orderBy = "date-desc");
        Task<JournalDto> Create(NewJournalModel model);

        Task<JournalDto> Update(long journalId, JournalUpdateModel updateModel);

        Task<bool> Delete(long journalId);
    }
}