using System.Threading.Tasks;
using Writely.Models;

namespace Writely.Services
{
    public interface ICredentialValidator
    {
        Task<bool> AreValidCredentials(Credentials credentials);
    }
}