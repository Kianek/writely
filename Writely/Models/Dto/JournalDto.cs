using System;
using System.Collections.Generic;
using System.Linq;

namespace Writely.Models.Dto
{
    public class JournalDto
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public List<EntryDto> Entries { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifed { get; set; }

        public JournalDto(Journal journal)
        {
            Id = journal.Id;
            UserId = journal.UserId;
            Title = journal.Title;
            CreatedAt = journal.CreatedAt;
            LastModifed = journal.LastModified;
            // TODO: use entry extension to project journal.Entries as dtos
            Entries = new List<EntryDto>();
        }
        
        public JournalDto()
        {
        }
    }
}