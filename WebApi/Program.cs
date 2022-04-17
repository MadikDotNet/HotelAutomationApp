using System.Threading.Tasks;
using HotelAutomationApp.Infrastructure.Interfaces.EmailServices;
using HotelAutomationApp.WebApi.Seeds;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelAutomationApp.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args)
                .Build();

            using var scope = host.Services.CreateScope();
            var seed = scope.ServiceProvider.GetRequiredService<Seed>();
            await seed.InitializeDb();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseUrls("http://localhost:80");
                });
    }
}