using System;
using System.Threading.Tasks;
using Writely.Data;

namespace Writely.UnitTests
{
    public abstract class RepositoryTestBase : IDisposable
    {
        private readonly DatabaseFixture _fixture;
        public AppDbContext Context { get; set; }

        public RepositoryTestBase()
        {
            _fixture = new DatabaseFixture();
        }

        public async Task PrepareDatabase()
        {
            var (options, operationalStoreOptions) = _fixture.CreateDbContextOptions();
            Context = new AppDbContext(options, operationalStoreOptions);
            await Context.Database.EnsureCreatedAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}