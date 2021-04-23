using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;

namespace Writely.Repositories
{
    public interface IEntryRepository : IRepository<Entry>
    {
        Task<IEnumerable<Entry>?> GetAllByTag(QueryFilter? filter);
    }
}