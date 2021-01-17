using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;

namespace Writely.Repositories
{
    public interface IEntryRepository
    {
        Task<Entry> GetById(string userId, long journalId, long entryId);
        Task<List<Entry>> GetAllByJournal(string userId, long journalId, string orderBy = "date-desc");
        Task<List<Entry>> GetAllByTag(string userId, string[] tags, string orderBy = "date-desc");
        Task<Entry> Save(Entry entry);
        Task<Entry> Update(Entry entry);
        Task Delete(long journalId, long entryId);
    }
}