using System.Data.Common;
using IdentityServer4.EntityFramework.Options;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Writely.Data;

namespace Writely.UnitTests
{
    public class DatabaseFixture
    {
        public DbConnection? Connection { get; private set; }

        public (DbContextOptions<AppDbContext>, 
            OptionsWrapper<OperationalStoreOptions>) CreateDbContextOptions()
        {
            var connectionString = "Data Source=:memory:";
            Connection = new SqliteConnection(connectionString);
            Connection.Open();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(Connection).Options;
            var operationalStoreOptions = 
                new OptionsWrapper<OperationalStoreOptions>(
                    new OperationalStoreOptions());
            
            return (options, operationalStoreOptions);
        }

        public void Dispose()
        {
            Connection?.Close();
        }
    }
}