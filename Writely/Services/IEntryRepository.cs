using System.Threading.Tasks;
using Writely.Models;

namespace Writely.Services
{
    public interface IEntryRepository
    {
        Task<Entry> GetById(long journalId, long entryId);
        Task<Entry> Update(IModelUpdater<Entry, EntryUpdateModel> updater, EntryUpdateModel model);
        Task<Entry> Save(Entry entry);
        Task Delete(long journalId, long entryId);
    }
}