using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;

namespace Writely.Repositories
{
    public interface IEntryRepository
    {
        Task<Entry> GetById(long entryId);
        Task<List<Entry>> GetAllByJournal(long journalId, string orderBy = "date-desc");
        Task<List<Entry>> GetAllByTag(long journalId, string[] tags, string orderBy = "date-desc");
        Task<Entry> Save(Entry entry);
        Task<Entry> Update(Entry entry);
        Task<bool> Delete(long journalId, long entryId);
    }
}