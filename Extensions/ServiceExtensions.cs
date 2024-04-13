using Microsoft.EntityFrameworkCore;
using TelegramBot_OpenAI.Data.DB;
using TelegramBot_OpenAI.Data.DB.Interfaces;
using TelegramBot_OpenAI.Data.DB.Repository;

namespace TelegramBot_OpenAI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TelegramBot_DbContext>(
                options => options.UseSqlServer(connectionString));

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
