using System;
using Writely.Models;

namespace Writely.Data
{
    public class Entity : ISortable
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public DateTime CreatedAt { get; }
        public DateTime LastModified { get; set; }

        public Entity(string? userId = null)
        {
            UserId = userId;
            CreatedAt = LastModified = DateTime.Now;
        }

        protected void UpdateLastModified() => LastModified = DateTime.Now;
    }
}