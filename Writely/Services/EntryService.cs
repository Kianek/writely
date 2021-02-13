using System.Collections.Generic;
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

        public EntryService(AppDbContext context, long? journalId)
        {
            _context = context;
            JournalId = journalId;
        }

        public async Task<Entry> GetById(long entryId)
        {
            return await GetUnitOfWork().Entries.GetById(entryId) 
                   ?? throw new EntryNotFoundException($"Entry not found: {entryId}");
        }

        public async Task<IEnumerable<Entry>?> GetAll()
        {
            return await GetUnitOfWork().Entries.GetAll();
        }

        public async Task<IEnumerable<Entry>?> GetAllByTag(string tags, string order = "date-desc")
        {
            return await GetUnitOfWork().Entries.GetAllByTag(tags.Split(","), order);
        }

        public async Task<Entry> Add(NewEntryModel model)
        {
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

        public async Task<Entry> Update(long entryId, EntryUpdateModel updateModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Entry> Remove(long entryId)
        {
            throw new System.NotImplementedException();
        }

        private IUnitOfWork GetUnitOfWork() => new UnitOfWork(_context, null, JournalId);
    }
}