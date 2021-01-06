using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;
using Writely.Models.Dto;
using Writely.Repositories;

namespace Writely.Services
{
    public class JournalService : IJournalService
    {
        private readonly IJournalRepository _repo;

        public JournalService(IJournalRepository repo)
        {
            _repo = repo;
        }

        public Task<JournalDto> GetById(string userId, long journalId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JournalDto>> GetAll(string userId, int limit = 0, string orderBy = "date-desc")
        {
            throw new System.NotImplementedException();
        }

        public Task<JournalDto> Save(NewJournalModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<JournalDto> Update(IModelUpdater<Entry, EntryUpdateModel> updater, EntryUpdateModel updateModel, long entryId)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(long journalId)
        {
            throw new System.NotImplementedException();
        }
    }
}