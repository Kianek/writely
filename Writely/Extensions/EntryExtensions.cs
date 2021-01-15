using System.Collections.Generic;
using System.Linq;
using Writely.Models;
using Writely.Models.Dto;

namespace Writely.Extensions
{
    public static class EntryExtensions
    {
        public static EntryDto ToDto(this Entry entry) => new EntryDto(entry);

        public static List<EntryDto> MapToDto(this List<Entry> entries)
            => entries.Select(e => e.ToDto()).ToList();
       
        
        public static IQueryable<Entry> SortBy(this IQueryable<Entry> entries, string order = "date-desc")
        {
            switch (order)
            {
                case "date-asc":
                    return entries.OrderBy(e => e.LastModified);
                case "asc":
                    return entries.OrderBy(e => e.Title);
                case "desc":
                    return entries.OrderByDescending(e => e.Title);
                default:
                    return entries.OrderByDescending(e => e.LastModified);
            }
        }
    }
}