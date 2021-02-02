using System;
using System.Collections.Generic;
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

        public static IOrderedEnumerable<T> SortBy<T>(this IEnumerable<T> sequence, string order = "date-desc")
            where T : ISortable
            => order switch
            {
                "asc" => sequence.OrderBy(entity => entity.Title),
                "desc" => sequence.OrderByDescending(entity => entity.Title),
                "date-asc" => sequence.OrderBy(entity => entity.LastModified),
                _ => sequence.OrderByDescending(entity => entity.LastModified),
            };
    }
}