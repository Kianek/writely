using System;

namespace Writely.Data
{
    public class Entity
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }

        public Entity(string userId)
        {
            UserId = userId;
        }

        public Entity()
        {
        }
    }
}