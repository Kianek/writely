using System;
using System.Collections.Generic;
using Writely.Extensions;

namespace Writely.Models.Dto
{
    public class JournalDto
    {
        public long Id { get; }
        public string UserId { get; }
        public string Title { get; }
        public List<EntryDto> Entries { get; }
        public DateTime CreatedAt { get; }
        public DateTime LastModified { get; }

        public JournalDto(Journal journal)
        {
            Id = journal.Id;
            UserId = journal.UserId!;
            Title = journal.Title!;
            CreatedAt = journal.CreatedAt;
            LastModified = journal.LastModified;
            Entries = journal.Entries.MapToDto();
        }
    }
}