namespace Writely.Models
{
    public class PasswordUpdate
    {
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

        public bool PasswordsMatch()
            => !(string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(ConfirmPassword))
                && Password == ConfirmPassword;
    }
}