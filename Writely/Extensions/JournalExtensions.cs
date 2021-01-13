using System;
using System.Collections.Generic;
using System.Linq;
using Writely.Models;
using Writely.Models.Dto;

namespace Writely.Extensions
{
    public static class JournalExtensions
    {
        public static JournalDto ToDto(this Journal journal)
        {
            return new JournalDto(journal);
        }
        
        public static List<JournalDto> MapToDto(this List<Journal> journals)
        {
            return journals.Select(j => j.ToDto()).ToList();
        }

        public static IQueryable<Journal> SortBy(this IQueryable<Journal> journals, string? order = null)
        {
            switch (order)
            {
                case "asc":
                    return journals.OrderBy(j => j.Title);
                case "desc":
                    return journals.OrderByDescending(j => j.Title);
                case "date-asc":
                    return journals.OrderBy(j => j.LastModified);
                default:
                    return journals.OrderByDescending(j => j.LastModified);
            }
        }
    }
}