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
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IJournalService, JournalService>();
            services.AddTransient<IEntryService, EntryService>();
        }
    }
}