using System;
using System.Collections.Generic;
using System.Linq;
using Writely.Data;

namespace Writely.Models
{
    public class Entry : Entity
    {
        public string? Tags { get; set; }
        public string? Body { get; set; }

        public long JournalId { get; set; }
        public Journal? Journal { get; set; }

        public Entry(string userId, long journalId, string title, string tags, string body) : base(userId)
        {
            JournalId = journalId;
            Title = title;
            Tags = tags;
            Body = body;
        }

        public Entry(long journalId, NewEntry model)
        {
            JournalId = journalId;
            Title = model.Title;
            Tags = model.Tags;
            Body = model.Body;
        }

        public Entry()
        {
        }

        public void AddTags(string newTags)
        {
            if (string.IsNullOrEmpty(newTags))
            {
                return;
            }

            Tags = Tags?.Length > 0
                ? Tags = SanitizeTags(Tags + "," + newTags)
                : Tags = SanitizeTags(newTags);
        }

        public bool Update(EntryUpdate model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var updatedProperties = new Dictionary<string, bool>();
            Title = Update(Title, model.Title, updatedProperties);
            Tags = Update(Tags, model.Tags, updatedProperties);
            Body = Update(Body, model.Body, updatedProperties);

            var didUpdate = updatedProperties.Values.Any(value => value);
            if (didUpdate) UpdateLastModified();

            return didUpdate;
        }

        private static string Update(string? property, string? updateProperty, IDictionary<string, bool> updatedProps)
        {
            if (updateProperty == null || property == updateProperty)
            {
                updatedProps[property!] = false;
                return property!;
            }

            updatedProps[property!] = true;
            return updateProperty;
        }

        public string[]? GetTags()
        {
            return Tags?.Split(",", StringSplitOptions.TrimEntries);
        }

        private static string SanitizeTags(string tags) =>
            string.Join(",",
                tags.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Distinct()
                    .ToArray());
    }
}