namespace Writely.Models
{
    public class UserAccountUpdateModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}