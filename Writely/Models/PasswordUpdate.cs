namespace Writely.Models
{
    public class PasswordUpdate
    {
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

        public void Deconstruct(out string? password, out string? confirmPassword)
        {
            password = Password;
            confirmPassword = ConfirmPassword;
        }
        
        public bool PasswordsMatch()
            => !(string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(ConfirmPassword))
                && Password == ConfirmPassword;
    }
}