using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace ReadingIsGood.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostBuilder => webHostBuilder.UseStartup<Startup>())
                .ConfigureLogging(loggingBuilder => loggingBuilder.ClearProviders())
                .UseNLog();
        }
    }
}
