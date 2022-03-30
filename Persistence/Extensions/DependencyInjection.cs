using HotelAutomationApp.Persistence.Context;
using HotelAutomationApp.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelAutomationApp.Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Default"),
                    builder => { builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null); });
            }
        );

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}