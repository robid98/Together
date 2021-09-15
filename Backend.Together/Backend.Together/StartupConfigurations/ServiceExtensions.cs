using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Together.Data.SQL;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Together.Data.Repositories.Interfaces;
using Together.Data.Repositories;
using Together.Services.Interfaces;
using Together.Services;
using Together.Data.Repositories.Repositories;
using Microsoft.OpenApi.Models;

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

        public static void AddInternalServices(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserAuthenticationRepository, UserAuthenticationRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();

            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
        }

        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend.Together", Version = "v1" });
            });
        }

        public static void AddNewtonsoftJsonService(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public static void AddAuthenticationService(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "http://localhost:5001",
                    ValidAudience = "http://localhost:5001",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                };
            });
        }
    }
}
