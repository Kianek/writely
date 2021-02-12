using System;
using System.Collections.Generic;
using Writely.Data;

namespace Writely.Models
{
    public class Journal : Entity
    {
        public List<Entry> Entries { get; set; } = new ();

        public Journal(string title, string userId) : base(userId)
        {
            Title = title;
        }
        
        public Journal() {}

        public void Add(Entry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException();
            }
            
            entry.JournalId = Id;
            entry.Journal = this;
            Entries.Add(entry);
            UpdateLastModified();
        }

        public bool Update(JournalUpdateModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var didUpdate = false;
            if (model.Title is not null && Title != model.Title)
            {
                Title = model.Title;
                didUpdate = true;
            }
            
            return didUpdate;
        }

        public bool Remove(Entry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException();
            }
            
            var removedSuccessfully = Entries.Remove(entry);
            if (removedSuccessfully)
            {
                UpdateLastModified();
            }

            return removedSuccessfully;
        }
    }
}