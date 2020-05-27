using System;
using System.IO;
using System.Reflection;
using EmailService;
using Legato.Contexts.Contracts;
using Legato.Contexts.Repositories;
using Legato.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Npgsql;
using AppContext = Legato.Contexts.AppContext;

namespace Legato.Extensions
{
    /// <summary>
    ///     The ServiceExtensions class contains extension methods to configure services in the Startup class
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        ///     The ConfigureSqlConnection method creates a connection between the application and the PostgreSQL server
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        public static void ConfigureSqlConnection(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("LEGATO_CONNECTION_STRING");

            services.AddDbContext<AppContext>(cfg =>
                cfg.UseNpgsql(connectionString ?? throw new NpgsqlException()));
        }

        /// <summary>
        ///     The ConfigureSwagger method configure the Swagger which provides the API's documentation
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Legato Web API",
                    Description =
                        "This is Legato's main web API which contains the logic and data tier and is responsible for the HTTP request-response handling.",
                    Contact = new OpenApiContact
                    {
                        Name = "Márk Paróczi",
                        Email = "mark.paroczi@gmail.com",
                        Url = new Uri("https://github.com/MParoczi")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(System.AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        ///     The ConfigureIdentity method configures the Identity for the API so the user's details are managed
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = true;
            }).AddEntityFrameworkStores<AppContext>();
        }

        /// <summary>
        ///     Configures the repository wrapper class and adds it to the services
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        /// <summary>
        ///     Configures CORS policy
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("_myAllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins(Environment.GetEnvironmentVariable("LEGATO_FRONTEND"))
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
        }

        /// <summary>
        /// Configures the EmailServices that handles the confirmation letter sending
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        public static void ConfigureEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            var emailConfig = configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            emailConfig.Password = Environment.GetEnvironmentVariable("LEGATO_EMAIL_PASSWORD");
            services.AddSingleton(emailConfig);
        }
    }
}