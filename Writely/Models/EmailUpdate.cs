namespace Writely.Models
{
    public class EmailUpdate
    {
        public string? Email { get; set; }

        public EmailUpdate()
        {
        }

        public EmailUpdate(string? email)
        {
            Email = email;
        }
    }
}