using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;
using Writely.Models.Dto;

namespace Writely.Services
{
    public interface IJournalService
    {
        Task<JournalDto> GetById(string userId, long journalId);
        Task<List<JournalDto>> GetAll(string userId, int limit = 0, string orderBy = "date-desc");
        Task<JournalDto> Save(NewJournalModel model);

        Task<JournalDto> Update(
            IModelUpdater<Entry, EntryUpdateModel> updater,
            EntryUpdateModel updateModel, long entryId);

        Task Delete(long journalId);
    }
}