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
        private readonly string _allowedOrigins = "_allowedOrigins";
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

            services.AddCors(opts =>
            {
                opts.AddPolicy(name: _allowedOrigins, builder =>
                {
                    builder.WithOrigins("https://writely.netlify.app");
                });
            });

            services.AddResponseCaching();

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
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseCors();

            app.UseResponseCaching();

            app.UseIdentityServer();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("api/sanity-check", async context =>
                {
                    await context.Response.WriteAsync("Hello, world!");
                });
                
                endpoints.MapControllers();
            });
        }
    }
}
