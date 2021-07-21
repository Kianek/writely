namespace Writely.Models
{
    public class PasswordUpdate : AccountUpdate
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        
        public PasswordUpdate(string userId, string? currentPassword, string? newPassword) 
            : base(userId)
        {
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }

        public void Deconstruct(out string? password, out string? newPassword)
        {
            password = CurrentPassword;
            newPassword = NewPassword;
        }
    }
}