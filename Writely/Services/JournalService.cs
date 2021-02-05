using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Data;
using Writely.Models;
using Writely.Models.Dto;

namespace Writely.Services
{
    public class JournalService : IJournalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public JournalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Journal> GetById(string userId, long journalId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Journal>> GetAll(int limit = 0, string orderBy = "date-desc")
        {
            throw new System.NotImplementedException();
        }

        public Task<Journal> Add(NewJournalModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<Journal> Update(long journalId, JournalUpdateModel updateModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Remove(long journalId)
        {
            throw new System.NotImplementedException();
        }
    }
}