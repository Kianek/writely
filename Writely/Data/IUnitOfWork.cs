using System;
using System.Threading.Tasks;
using Writely.Models;
using Writely.Repositories;

namespace Writely.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Journal> Journals { get; }
        IEntryRepository Entries { get; }
        Task<int> Complete();
    }
}