using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Writely.Data;
using Writely.Extensions;

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
            
            services.AddDbContext<AppDbContext>(opts =>
            {
                var dbPassword = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");
                var connectionString = Configuration.GetConnectionString("Writely");
                opts.UseSqlServer($"{connectionString};Password={dbPassword}");
            });

            services.AddIdentityServerAuthenticationWithJwt();
            
            services.AddWritelyServices();

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

            app.UseIdentityServer();

            app.UseAuthentication();
            
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
