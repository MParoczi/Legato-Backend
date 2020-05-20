using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Legato
{
#pragma warning disable CS1591
    /// <summary>
    ///     ASP.NET Core web application is actually a console project which starts executing from the entry point public
    ///     static void Main() in Program class where we can create a host for the web application.
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     The Main method is the entry point of the Legato Web API.
        /// </summary>
        /// <param name="args">The parameter of the Main method is a String array that represents the command-line arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        ///     The default implementation of this method looks for a public static IHostBuilder CreateHostBuilder(string[] args)
        ///     method defined on the entry point of the assembly of TEntryPoint and invokes it passing an empty string array as
        ///     arguments.
        /// </summary>
        /// <param name="args">Empty string array as arguments</param>
        /// <returns>Returns a IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
#pragma warning restore CS1591
}