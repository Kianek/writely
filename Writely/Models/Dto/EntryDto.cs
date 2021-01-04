using System;

namespace Writely.Models.Dto
{
    public class EntryDto
    {
        public long Id { get; set; }
        public long JournalId { get; set; }
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public string? Tags { get; set; }
        public string? Body { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModified { get; set; }
        
        public EntryDto(Entry entry)
        {
            Id = entry.Id;
            JournalId = entry.JournalId;
            UserId = entry.UserId;
            Title = entry.Title;
            Tags = entry.Tags;
            Body = entry.Body;
            CreatedAt = entry.CreatedAt;
            LastModified = entry.LastModified;
        }

        public EntryDto()
        {
        }
    }
}