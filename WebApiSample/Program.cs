using FirstMvcApp.Core.SerilogSinks;
using Serilog;

namespace WebApiSample
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((ctx, lc) =>
                {
                    lc.MinimumLevel.Debug().WriteTo.CustomSink();
                    //lc.MinimumLevel.Warning().WriteTo.File(@"C:\Users\AlexiMinor\Desktop\New folder (4)\log.log");
                }).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }
    }
}

