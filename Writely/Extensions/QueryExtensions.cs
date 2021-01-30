using System;
using System.Linq;
using Writely.Models;

namespace Writely.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<ISortable> SortBy(this IQueryable<ISortable> query, string order = "date-desc")
        {
            switch (order)
            {
                case "asc": return query.OrderBy(entity => entity.Title);
                case "desc": return query.OrderByDescending(entity => entity.Title);
                case "date-asc": return query.OrderBy(entity => entity.LastModified);
                default: return query.OrderByDescending(entity => entity.LastModified);
            }
        }
    }
}