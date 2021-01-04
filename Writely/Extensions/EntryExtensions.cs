
using System;
using System.Collections.Generic;
using Writely.Models;
using Writely.Models.Dto;

namespace Writely.Extensions
{
    public static class EntryExtensions
    {
        public static EntryDto ToDto(this Entry entry)
        {
            return new EntryDto(entry);
        }

        public static List<EntryDto> MapToDto(this List<Entry> entries)
        {
            throw new NotImplementedException();
        }
    }
}