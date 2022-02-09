using System.Collections.Generic;
using System.Threading.Tasks;
using HotelAutomationApp.Application.File.Models;
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

            var mediator = scope.ServiceProvider.GetService<IMediator>();
            await mediator.Send(new UnbindRoomMediaCommand("48a2829f-1da1-4c6a-96b3-2fdfd53f0476",
                new List<string>
                {
                    "6060a35e-0b3e-4afd-abcd-63dca6f8a0e4"
                }));
            await seed.InitializeDb();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}