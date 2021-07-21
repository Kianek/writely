namespace Writely.Models
{
    public class EmailUpdate : AccountUpdate
    {
        public string? Email { get; set; }

        public EmailUpdate(string userId, string? email) : base(userId)
        {
            Email = email;
        }
    }
}