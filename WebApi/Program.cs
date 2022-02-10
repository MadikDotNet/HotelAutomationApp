using System.Collections.Generic;
using System.Threading.Tasks;
using HotelAutomationApp.Application.MediaFiles.Commands;
using HotelAutomationApp.Application.RoomMedia.Commands;
using HotelAutomationApp.WebApi.Seeds;
using MediatR;
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
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}