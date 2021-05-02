namespace Writely.Models
{
    public class PasswordUpdate
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }

        public void Deconstruct(out string? password, out string? newPassword)
        {
            password = CurrentPassword;
            newPassword = NewPassword;
        }
    }
}