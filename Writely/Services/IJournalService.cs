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

        Task<IEnumerable<Entry>?> GetEntriesByJournal(long journalId, QueryFilter filter);
        
        Task<Journal> Add(NewJournal model);

        Task<int> Update(long journalId, JournalUpdate updateModel);

        Task<int> Remove(long journalId);

        Task<int> RemoveAllByUser(string userId);
    }
}