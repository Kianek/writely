using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Writely.Data;
using Writely.Services;

namespace Writely.Extensions
{
    public static class WritelyServices
    {
        public static void AddWritelyServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IJournalService, JournalService>();
            services.AddTransient<IEntryService, EntryService>();
        }

        public static void AddIdentityServerAuthenticationWithJwt(this IServiceCollection services)
        {
            services.AddDefaultIdentity<AppUser>()
                .AddEntityFrameworkStores<AppDbContext>();
            
            services.AddIdentityServer()
                .AddApiAuthorization<AppUser, AppDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
        }
    }
}