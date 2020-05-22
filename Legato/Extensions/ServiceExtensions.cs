using System;
using Legato.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

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
        /// <param name="services"></param>
        public static void ConfigureSqlConnection(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("LEGATO_CONNECTION_STRING");

            services.AddDbContext<IdentityAppContext>(cfg =>
                cfg.UseNpgsql(connectionString ?? throw new NpgsqlException()));
        }
    }
}