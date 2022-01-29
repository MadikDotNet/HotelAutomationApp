using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HotelAutomationApp.Shared.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMediatrHandlers(this IServiceCollection services, Assembly assembly)
        {
            var handlerClasses = assembly.DefinedTypes
                .Select(q => q.GetTypeInfo())
                .Where(q => q.IsClass && !q.IsAbstract && !q.IsGenericTypeDefinition);

            foreach (var handlerClass in handlerClasses)
            {
                var handlerInterfaces = handlerClass.ImplementedInterfaces
                    .Select(q => q.GetTypeInfo())
                    .Where(q => q.IsGenericType &&
                                q.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) &&
                                !q.IsGenericTypeDefinition).ToList();

                foreach (var handlerInterface in handlerInterfaces)
                {
                    services.AddTransient(handlerInterface.AsType(), handlerClass.AsType());
                }
            }

            return services;
        }
    }
}