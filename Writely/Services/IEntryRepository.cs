using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;

namespace Writely.Services
{
    public interface IEntryRepository
    {
        Task<Entry> GetById(long entryId);
        Task<List<Entry>> GetAllByJournal(long journalId);
        Task<Entry> Save(NewEntryModel entry);
        Task<Entry> Update(Entry entry);
        Task Delete(long journalId, long entryId);
    }
}