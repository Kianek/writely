using System;
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

        public string[]? GetTags()
        {
            return Tags?.Split(",", StringSplitOptions.TrimEntries);
        }
    }
}