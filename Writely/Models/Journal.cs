using System.Collections.Generic;
using Writely.Data;

namespace Writely.Models
{
    public class Journal : Entity
    {
        public string? Title { get; set; }
        public List<Entry> Entries { get; set; } = new ();

        public Journal(string title, string userId) : base(userId)
        {
            Title = title;
        }
        
        public Journal() {}
    }
}