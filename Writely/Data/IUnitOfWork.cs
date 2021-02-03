using System;
using Writely.Models;
using Writely.Repositories;

namespace Writely.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Journal> Journals { get; }
        IEntryRepository Entries { get; }
        int Complete();
    }
}