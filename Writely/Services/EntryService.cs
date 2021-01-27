using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public Task<List<EntryDto>> GetAllByTag(long journalId, string tags)
        {
            throw new System.NotImplementedException();
        }

        public Task<EntryDto> Create(Entry model)
        {
            throw new System.NotImplementedException();
        }

        public Task<EntryDto> Update(long entryId, EntryUpdateModel updateModel)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(long journalId, long entryId)
        {
            throw new System.NotImplementedException();
        }
    }
}