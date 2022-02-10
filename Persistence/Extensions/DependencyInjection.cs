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
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        return services;
    }
}