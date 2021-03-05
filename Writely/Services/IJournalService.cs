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
        Task<IEnumerable<Journal>?> GetAll(int limit = 0, string orderBy = "date-desc");
        Task<Journal> Add(NewJournalModel model);

        Task<Journal> Update(long journalId, JournalUpdate updateModel);

        Task<Journal> Remove(long journalId);
    }
}