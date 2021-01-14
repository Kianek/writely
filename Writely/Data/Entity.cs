using System;

namespace Writely.Data
{
    public class Entity
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedAt { get; }
        public DateTime LastModified { get; set; }

        public Entity(string? userId = null)
        {
            UserId = userId;
            CreatedAt = LastModified = DateTime.Now;
        }
    }
}