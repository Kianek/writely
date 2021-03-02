namespace Writely.Models
{
    public class AccountUpdate
    {
        public string UserId { get; set; }
        public EmailUpdate? EmailUpdate { get; set; }
        public PasswordUpdate? PasswordUpdate { get; set; }

        protected AccountUpdate(string userId, EmailUpdate emailUpdate = null!,
            PasswordUpdate passwordUpdate = null!)
        {
            UserId = userId;
            EmailUpdate = emailUpdate;
            PasswordUpdate = passwordUpdate;
        }
    }
}