using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Scrutor;
using Writely.Data;
using Writely.Models;
using Writely.Services;

namespace Writely
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<IModelUpdater<Entry, EntryUpdateModel>>()
                .AddClasses(classes => classes.AssignableTo(typeof(IModelUpdater<,>)))
                .AsImplementedInterfaces());
            
            services.AddDbContext<AppDbContext>(opts =>
            {
                var dbPassword = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");
                var connectionString = Configuration.GetConnectionString("Writely");
                opts.UseSqlServer($"{connectionString};Password={dbPassword}");
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/sanity-check", async context =>
                {
                    await context.Response.WriteAsync("Hello, world!");
                });
                
                endpoints.MapControllers();
            });
        }
    }
}
