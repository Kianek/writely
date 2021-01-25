using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public Task<JournalDto> Update([FromServices] IModelUpdater<Journal, JournalUpdateModel> updater,
                                                   JournalUpdateModel updateModel, long journalId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(long journalId)
        {
            throw new System.NotImplementedException();
        }
    }
}