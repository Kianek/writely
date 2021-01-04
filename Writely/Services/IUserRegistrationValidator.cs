using Writely.Models;

namespace Writely.Services
{
    public interface IUserRegistrationValidator
    {
        bool ValidateRegistrationModel(UserRegistrationModel model);
    }
}