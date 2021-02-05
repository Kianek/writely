using System;
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

        public Entry(string title, string? tags, string body, string userId) : base(userId)
        {
            Title = title;
            Tags = tags ?? "";
            Body = body;
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