using Legato.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Legato
{
#pragma warning disable CS1591
    /// <summary>
    ///     ASP.NET Core apps use a Startup class, which is named Startup by convention. The Startup class:
    ///     <list type="bullet">
    ///         <item>
    ///             <description>
    ///                 Optionally includes a ConfigureServices method to configure the app's services.A service is a
    ///                 reusable component that provides app functionality.Services are registered in ConfigureServices
    ///                 and consumed across the app via dependency injection (DI) or ApplicationServices.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 Includes a Configure method to create the app's request processing pipeline.
    ///             </description>
    ///         </item>
    ///     </list>
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Constructor for the Startup class that is called in the CreateHostBuilder method in the Program class.
        /// </summary>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        ///     Represents a set of key/value application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     The ConfigureServices method is a place where you can register your dependent classes with the built-in IoC
        ///     container. After registering dependent class, it can be used anywhere in the application. You just need to include
        ///     it in the parameter of the constructor of a class where you want to use it. The IoC container will inject it
        ///     automatically.
        /// </summary>
        /// <param name="services">Container for the dependencies</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.ConfigureSwagger();

            services.ConfigureSqlConnection();

            services.ConfigureIdentity();

            services.ConfigureRepository();

            services.ConfigureCors();
        }

        /// <summary>
        ///     The Configure method is a place where you can configure application request pipeline for your application using
        ///     IApplicationBuilder instance that is provided by the built-in IoC container.
        /// </summary>
        /// <param name="app">IApplicationBuilder instance that is provided by the built-in IoC container</param>
        /// <param name="env">Provides information about the web hosting environment an application is running in.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Legato");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
#pragma warning restore CS1591
}