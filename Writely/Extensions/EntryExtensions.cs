using System;
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
       

        public static bool Update(this Entry entry, EntryUpdateModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException();
            }
            
            var didUpdate = false;
            if (model.Title is not null && entry.Title != model.Title)
            {
                entry.Title = model.Title;
                didUpdate = true;
            }

            if (model.Tags is not null && entry.Tags != model.Tags)
            {
                entry.Tags = model.Tags;
                didUpdate = true;
            }

            if (model.Body is not null && entry.Body != model.Body)
            {
                entry.Body = model.Body;
                didUpdate = true;
            }
            
            return didUpdate;
        }
        
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