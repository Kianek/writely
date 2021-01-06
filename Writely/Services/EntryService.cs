using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;
using Writely.Models.Dto;
using Writely.Repositories;

namespace Writely.Services
{
    public class EntryService : IEntryService
    {
        private readonly IEntryRepository _repo;

        public EntryService(IEntryRepository repo)
        {
            _repo = repo;
        }

        public Task<EntryDto> GetById(long entryId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<EntryDto>> GetAllByJournal(long journalId)
        {
            throw new System.NotImplementedException();
        }

        public Task<EntryDto> Save(Entry model)
        {
            throw new System.NotImplementedException();
        }

        public Task<EntryDto> Update(IModelUpdater<Entry, EntryUpdateModel> updater, EntryUpdateModel updateModel, long entryId)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(long journalId, long entryId)
        {
            throw new System.NotImplementedException();
        }
    }
}