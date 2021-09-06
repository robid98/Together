using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Together.Data.SQL;
using Microsoft.Extensions.Configuration;

namespace Together.API.StartupConfigurations
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            /* Allow Cors */
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder.WithOrigins("http://localhost:4200/"));
            });
        }

        public static void AddDatabaseContenxt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
                  options.UseSqlServer(configuration.GetConnectionString("TogetherDatabase")));
        }
    }
}
