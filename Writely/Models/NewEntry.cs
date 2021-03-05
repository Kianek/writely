namespace Writely.Models
{
    public class NewEntry
    {
        public string? UserId { get; set; }
        public long JournalId { get; set; }
        public string? Title { get; set; }
        public string? Tags { get; set; }
        public string? Body { get; set; }
    }
}