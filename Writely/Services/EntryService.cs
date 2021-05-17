using System;
using Writely.Exceptions;
using Writely.Models;

namespace Writely.Services
{
    public class EntryService : IEntryService
    {
        public Entry? GetById(Journal journal, long entryId)
        {
            var entry = journal.Entries.Find(e => e.Id == entryId);
            if (entry is null)
            {
                throw new EntryNotFoundException($"Entry not found: {entryId}");
            }
            
            return entry;
        }

        public Entry Add(Journal journal, NewEntry model)
        {
            if (journal is null)
            {
                throw new ArgumentNullException(nameof(journal));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            
            var newEntry = new Entry(journal.Id, model);
            journal.Entries.Add(newEntry);
            return newEntry;
        }

        public Entry Update(Journal journal, long entryId, EntryUpdate updateModel)
        {
            if (journal is null)
            {
                throw new ArgumentNullException(nameof(journal));
            }
            
            var entry = journal.Entries.Find(e => e.Id == entryId);
            if (entry is null)
            {
                throw new EntryNotFoundException($"Entry not found");
            }
            
            entry.Update(updateModel);
            return entry;
        }

        public Entry Remove(Journal journal, long entryId)
        {
            if (journal is null)
            {
                throw new ArgumentNullException(nameof(journal));
            }
            
            var entry = journal.Entries.Find(e => e.Id == entryId);
            if (entry is null)
            {
                throw new EntryNotFoundException($"Entry not found: {entryId}");
            }
            
            journal.Entries.Remove(entry);
            return entry;
        }
    }
}