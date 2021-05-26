namespace Writely.Models
{
    public record NewEntry(
        string UserId,
        long JournalId,
        string? Title,
        string? Tags,
        string? Body);
}