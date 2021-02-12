using System;
using System.Collections.Generic;
using System.Linq;
using Writely.Models;
using Writely.Models.Dto;

namespace Writely.Extensions
{
    public static class JournalExtensions
    {
        public static JournalDto ToDto(this Journal journal) => new JournalDto(journal);
        
        public static List<JournalDto> MapToDto(this List<Journal> journals) 
            => journals.Select(j => j.ToDto()).ToList();
    }
}