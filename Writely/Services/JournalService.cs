using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Writely.Data;
using Writely.Exceptions;
using Writely.Models;
using Writely.Models.Dto;

namespace Writely.Services
{
    public class JournalService : IJournalService
    {
        private readonly AppDbContext _context;
        private readonly IEntryService _entryService;
        
        public string? UserId { get; set; }

        public JournalService(AppDbContext context, IEntryService entryService)
        {
            _context = context;
            _entryService = entryService;
        }

        public async Task<Journal> GetById(long journalId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                throw new UserNotFoundException();
            }
            
            using var unitOfWork = GetUnitOfWork();
            var journal = await unitOfWork.Journals.GetById(journalId);
            return journal ?? throw new JournalNotFoundException($"Journal not found: {journalId}");
        }

        public async Task<IEnumerable<Entry>?> GetEntriesByJournal(long journalId, QueryFilter filter)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                throw new UserNotFoundException();
            }
            
            using var unitOfWork = GetUnitOfWork(journalId);
            
            return await unitOfWork.Entries.GetAll(filter);
        }

        public async Task<IEnumerable<Journal>?> GetAll(QueryFilter? filter)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                throw new UserNotFoundException();
            }
            
            using var unitOfWork = GetUnitOfWork();
            return await unitOfWork.Journals.GetAll(filter);
        }

        public async Task<Journal> Add(NewJournal model)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                throw new UserNotFoundException();
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
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

        public async Task<int> RemoveAllByUser(string userId)
        {
            var journals = await _context.Journals
                .Where(j => j.UserId == userId)
                    .Include(j => j.Entries)
                .ToListAsync();
            
            _context.Journals.RemoveRange(journals);
            return await _context.SaveChangesAsync();
        }
        
        // Entry-specific methods

        public async Task<Entry> GetEntry(long journalId, long entryId)
        {
            using var unitOfWork = GetUnitOfWork();
            var journal = await unitOfWork
                .Journals.Find(j => j.Id == journalId);
            if (journal is null)
            {
                throw new JournalNotFoundException($"Journal not found: {journalId}");
            }

            return _entryService.GetById(journal, entryId)!;
        }

        public async Task<IEnumerable<Entry>?> GetAllEntries(long journalId, QueryFilter filter)
        {
            using var unitOfWork = GetUnitOfWork();
            var journal = await unitOfWork
                .Journals.Find(j => j.Id == journalId);
            if (journal is null)
            {
                throw new JournalNotFoundException($"Journal not found: {journalId}");
            }

            return journal.Entries;
        }

        public async Task<Entry> AddEntry(long journalId, NewEntry newEntry)
        {
            using var unitOfWork = GetUnitOfWork();
            var journal = await unitOfWork
                .Journals.Find(j => j.Id == journalId);
            if (journal is null)
            {
                throw new JournalNotFoundException($"Journal not found: {journalId}");
            }

            if (newEntry is null)
            {
                throw new ArgumentNullException(nameof(newEntry));
            }
            var entry = _entryService.Add(journal, newEntry);
            await unitOfWork.Complete();
            
            return entry;
        }

        public async Task<Entry> UpdateEntry(long journalId, long entryId, EntryUpdate updateModel)
        {
            if (updateModel is null)
            {
                throw new ArgumentNullException(nameof(updateModel));
            }
            
            using var unitOfWork = GetUnitOfWork();
            var journal = await unitOfWork
                .Journals.Find(j => j.Id == journalId);
            if (journal is null)
            {
                throw new JournalNotFoundException($"Journal not found: {journalId}");
            }
            
            var entry = _entryService.Update(journal, entryId, updateModel);
            await unitOfWork.Complete();
            
            return entry;
        }

        public async Task<Entry> RemoveEntry(long journalId, long entryId)
        {
            using var unitOfWork = GetUnitOfWork();
            var journal = await unitOfWork
                .Journals.Find(j => j.Id == journalId);
            if (journal is null)
            {
                throw new JournalNotFoundException($"Journal not found: {journalId}");
            }
            
            var entry = _entryService.Remove(journal, entryId);
            await unitOfWork.Complete();
            return entry;
        }

        private IUnitOfWork GetUnitOfWork(long? journalId = null) 
            => new UnitOfWork(_context) { UserId = UserId, JournalId = journalId};
    }
}