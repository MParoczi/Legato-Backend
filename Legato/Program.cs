using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Legato
{
    /// <summary>
    ///     ASP.NET Core web application is actually a console project which starts executing from the entry point public
    ///     static void Main() in Program class where we can create a host for the web application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The Main method is the entry point of the Legato Web API.
        /// </summary>
        /// <param name="args">The parameter of the Main method is a String array that represents the command-line arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}