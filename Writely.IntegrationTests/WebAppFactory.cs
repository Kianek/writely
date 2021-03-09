using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Writely.Data;
using Writely.Extensions;

namespace Writely.IntegrationTests
{
    public class WebAppFactory<TStartup> 
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<AppDbContext>));
                services.Remove(descriptor);
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlite("Data Source=:memory:"));
                services.AddWritelyServices();

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<WebAppFactory<TStartup>>>();

                db.Database.EnsureCreated();

                try
                {
                    SeedData.InitializeDatabase(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error seeding the database. Error: {Message}", ex.Message);
                }
            });
        }
    }
}