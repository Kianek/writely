using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;

namespace Writely.Services
{
    public interface IEntryService
    {
        long? JournalId { get; set; }
        Task<Entry> GetById(long entryId);
        Task<IEnumerable<Entry>?> GetAll();
        Task<IEnumerable<Entry>?> GetAllByTag(string tags, string orderBy = "date-desc");
        Task<Entry> Add(NewEntry model);
        Task<Entry> Update(long entryId, EntryUpdate updateModel);
        Task<Entry> Remove(long entryId);
    }
}