using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Legato
{
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

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
            });
        }

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}