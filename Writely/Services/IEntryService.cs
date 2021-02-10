using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;

namespace Writely.Services
{
    public interface IEntryService
    {
        long JournalId { get; set; }
        Task<Entry> GetById(long entryId);
        Task<IEnumerable<Entry>> GetAllByJournal(long journalId);
        Task<IEnumerable<Entry>> GetAllByTag(long journalId, string tags, string orderBy = "date-desc");
        Task<Entry> Add(NewEntryModel model);
        Task<Entry> Update(long entryId, EntryUpdateModel updateModel);
        Task<Entry> Remove(long entryId);
    }
}