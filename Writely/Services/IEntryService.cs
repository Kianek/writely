using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;

namespace Writely.Services
{
    public interface IEntryService
    {
        long? JournalId { get; set; }
        Task<Entry> GetById(long entryId);
        Task<Entry> Add(NewEntry model);
        Task<Entry> Update(long entryId, EntryUpdate updateModel);
        Task<Entry> Remove(long entryId);
    }
}