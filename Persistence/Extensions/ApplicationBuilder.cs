using HotelAutomationApp.Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotelAutomationApp.Persistence.Extensions;

public static class ApplicationBuilder
{
    public static void InitializeApplicationDb(this IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var applicationDb = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        applicationDb.Database.EnsureCreated();
        applicationDb.Database.Migrate();
    }
}