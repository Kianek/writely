using System;

namespace Writely.Models.Dto
{
    public class EntryDto
    {
        public long Id { get;  }
        public long JournalId { get;  }
        public string UserId { get;  }
        public string Title { get;  }
        public string Tags { get;  }
        public string Body { get; }
        public DateTime CreatedAt { get; }
        public DateTime LastModified { get; }
        
        public EntryDto(Entry entry)
        {
            Id = entry.Id;
            JournalId = entry.JournalId;
            UserId = entry.UserId ?? "";
            Title = entry.Title ?? "";
            Tags = entry.Tags ?? "";
            Body = entry.Body ?? "";
            CreatedAt = entry.CreatedAt;
            LastModified = entry.LastModified;
        }
    }
}