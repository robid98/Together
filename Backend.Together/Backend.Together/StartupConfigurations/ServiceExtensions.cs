using Microsoft.Extensions.DependencyInjection;

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
    }
}
