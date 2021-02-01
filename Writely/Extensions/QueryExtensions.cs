using System;
using System.Linq;
using Writely.Models;

namespace Writely.Extensions
{
    public static class QueryExtensions
    {
        public static IOrderedQueryable<T> SortBy<T>(this IQueryable<T> query, string order = "date-desc")
            where T : ISortable
            => order switch
            {
                "asc" => query.OrderBy(entity => entity.Title),
                "desc" => query.OrderByDescending(entity => entity.Title),
                "date-asc" => query.OrderBy(entity => entity.LastModified),
                _ => query.OrderByDescending(entity => entity.LastModified)
            };
    }
}