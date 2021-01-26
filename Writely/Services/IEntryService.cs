using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;
using Writely.Models.Dto;

namespace Writely.Services
{
    public interface IEntryService
    {
        Task<EntryDto> GetById(long entryId);
        Task<List<EntryDto>> GetAllByJournal(long journalId);
        Task<List<EntryDto>> GetAllByTag(long journalId, string tags);
        Task<EntryDto> Save(Entry model);
        Task<EntryDto> Update(long entryId, EntryUpdateModel updateModel);
        Task Delete(long journalId, long entryId);
    }
}