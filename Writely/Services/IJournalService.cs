using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;
using Writely.Models.Dto;

namespace Writely.Services
{
    public interface IJournalService
    {
        string? UserId { get; set; }
        
        Task<Journal> GetById(long journalId);
        
        Task<IEnumerable<Journal>?> GetAll(QueryFilter filter);

        Task<Journal> Add(NewJournal model);

        Task<int> Update(long journalId, JournalUpdate updateModel);

        Task<int> Remove(long journalId);

        Task<int> RemoveAllByUser(string userId);
        
        // Entry-specific methods
        Task<Entry> GetEntry(long journalId, long entryId);
        
        Task<IEnumerable<Entry>?> GetAllEntries(long journalId, QueryFilter filter);

        Task<Entry> AddEntry(long journalId, NewEntry newEntry);

        Task<Entry> UpdateEntry(long journalId, long entryId, EntryUpdate updateModel);

        Task<Entry> RemoveEntry(long journalId, long entryId);
    }
}