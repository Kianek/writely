namespace Writely.Models
{
    public abstract class AccountUpdate
    {
        public string UserId { get; set; }
        
        public AccountUpdate(string userId) => UserId = userId;
    }
}