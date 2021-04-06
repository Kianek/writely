using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Data;
using Writely.Exceptions;
using Writely.Models;
using Writely.Models.Dto;

namespace Writely.Services
{
    public class JournalService : IJournalService
    {
        private readonly AppDbContext _context;
        
        public string? UserId { get; set; }

        public JournalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Journal> GetById(long journalId)
        {
            if (UserId == null)
            {
                throw new UserNotFoundException();
            }
            
            using var unitOfWork = GetUnitOfWork();
            var journal = await unitOfWork.Journals.GetById(journalId);
            return journal ?? throw new JournalNotFoundException($"Journal not found: {journalId}");
        }

        public async Task<IEnumerable<Journal>?> GetAll(int limit = 0, string orderBy = "date-desc")
        
        {
            if (UserId == null)
            {
                throw new UserNotFoundException();
            }
            
            using var unitOfWork = GetUnitOfWork();
            return await unitOfWork.Journals.GetAll(null, orderBy, limit);
        }

        public async Task<Journal> Add(NewJournal model)
        {
            if (UserId == null)
            {
                throw new UserNotFoundException();
            }

            if (model == null)
            {
                throw new ArgumentNullException();
            }
            
            var unitOfWork = GetUnitOfWork();
            var newJournal = new Journal {Title = model.Title, UserId = UserId};
            unitOfWork.Journals.Add(newJournal);
            await unitOfWork.Complete();
            return newJournal;
        }

        public async Task<int> Update(long journalId, JournalUpdate updateModel)
        {
            using var unitOfWork = GetUnitOfWork();
            var journal = await unitOfWork.Journals.GetById(journalId);
            if (journal == null)
            {
                throw new JournalNotFoundException($"Journal not found: {journalId}");
            }
            
            journal.Update(updateModel);
            return await unitOfWork.Complete();
        }

        public async Task<int> Remove(long journalId)
        {
            using var unitOfWork = GetUnitOfWork();
            var journal = await unitOfWork.Journals.GetById(journalId);
            if (journal == null)
            {
                throw new JournalNotFoundException($"Journal not found: {journalId}");
            }
            
            unitOfWork.Journals.Remove(journal);
            return await unitOfWork.Complete();
        }

        private IUnitOfWork GetUnitOfWork(long? journalId = null) 
            => new UnitOfWork(_context) { UserId = UserId, JournalId = journalId};
    }
}