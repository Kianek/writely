using System;

namespace Writely.Models
{
    public class Entity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
    }
}