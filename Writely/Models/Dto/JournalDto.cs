using System;
using System.Collections.Generic;
using Writely.Extensions;

namespace Writely.Models.Dto
{
    public class JournalDto
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public List<EntryDto> Entries { get; set; } = new List<EntryDto>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifed { get; set; }

        public JournalDto(Journal journal)
        {
            Id = journal.Id;
            UserId = journal.UserId;
            Title = journal.Title;
            CreatedAt = journal.CreatedAt;
            LastModifed = journal.LastModified;
            Entries = journal.Entries.MapToDto();
        }
        
        public JournalDto()
        {
        }
    }
}