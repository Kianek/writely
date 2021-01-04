using Writely.Models;

namespace Writely.Services
{
    public interface IModelValidator<T>
    {
        bool ValidateRegistrationModel(T model);
    }
}