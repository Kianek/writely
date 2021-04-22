using System;
using System.Threading.Tasks;
using Writely.Models;
using Writely.Repositories;

namespace Writely.Data
{
    public interface IUnitOfWork : IDisposable
    {
        string? UserId { get; set; }
        long? JournalId { get; set; }
        IRepository<Journal> Journals { get; }
        IEntryRepository Entries { get; }
        Task<int> Complete();
    }
}