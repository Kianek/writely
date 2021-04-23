using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Writely.Data;
using Writely.Exceptions;
using Writely.Models;

namespace Writely.Services
{
    public class EntryService : IEntryService
    {
        private AppDbContext _context;
        
        public long? JournalId { get; set; }

        public EntryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Entry> GetById(long entryId)
        {
            return await GetUnitOfWork().Entries.GetById(entryId) 
                   ?? throw new EntryNotFoundException($"Entry not found: {entryId}");
        }

        public async Task<IEnumerable<Entry>?> GetAllByTag(QueryFilter? filter)
        {
            return await GetUnitOfWork().Entries.GetAllByTag(filter);
        }

        public async Task<Entry> Add(NewEntry model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (JournalId == null)
            {
                throw new JournalNotFoundException();
            }
            
            var unitOfWork = GetUnitOfWork();
            var journal = await unitOfWork.Journals.GetById(JournalId.GetValueOrDefault());
            if (journal == null)
            {
                throw new JournalNotFoundException();
            }
            var entry = new Entry
            {
                Title = model.Title,
                Tags = model.Tags,
                Body = model.Body
            };
            journal.Add(entry);
            await unitOfWork.Complete();
            
            return entry;
        }

        public async Task<Entry> Update(long entryId, EntryUpdate updateModel)
        {
            if (updateModel == null)
            {
                throw new ArgumentNullException(nameof(updateModel));
            }
            
            using var unitOfWork = GetUnitOfWork();
            var entry = await unitOfWork.Entries.GetById(entryId);
            if (entry == null)
            {
                throw new EntryNotFoundException($"Entry not found: {entryId}");
            }
            
            if (entry.Update(updateModel))
            {
                await unitOfWork.Complete();
            }

            return entry;
        }

        public async Task<Entry> Remove(long entryId)
        {
            using var unitOfWork = GetUnitOfWork();
            var journal = await unitOfWork.Journals.GetById(JournalId.GetValueOrDefault());
            if (journal == null)
            {
                throw new JournalNotFoundException($"Journal not found: {JournalId}");
            }

            var entry = journal.Entries.Find(e => e.Id == entryId);
            if (entry == null)
            {
                throw new EntryNotFoundException($"Entry not found: {entryId}");
            }
            journal.Entries = journal.Entries.SkipWhile(e => e.Id == entryId).ToList();
            await unitOfWork.Complete();

            return entry;
        }

        private IUnitOfWork GetUnitOfWork() 
            => new UnitOfWork(_context) { JournalId = JournalId};
    }
}