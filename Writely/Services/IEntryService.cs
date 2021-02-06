using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;

namespace Writely.Services
{
    public interface IEntryService
    {
        long JournalId { get; set; }
        Task<Entry> GetById(long entryId);
        Task<List<Entry>> GetAllByJournal(long journalId);
        Task<List<Entry>> GetAllByTag(long journalId, string tags);
        Task<Entry> Add(NewEntryModel model);
        Task<Entry> Update(long entryId, EntryUpdateModel updateModel);
        Task Remove(long entryId);
    }
}