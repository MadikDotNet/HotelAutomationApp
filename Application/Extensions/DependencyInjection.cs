using HotelAutomationApp.Application.ApplicationServices.Dictionary;
using Microsoft.Extensions.DependencyInjection;

namespace HotelAutomationApp.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services) =>
        RegisterDictionaryCrudServices(services);
    
    public static IServiceCollection RegisterDictionaryCrudServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(DictionaryCrudService<,>));
        services.AddScoped(typeof(TreeDictionaryCrudService<,>));

        return services;
    }
}