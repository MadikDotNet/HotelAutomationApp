using System.Threading.Tasks;
using HotelAutomationApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.WebApi.Seeds;

public class Seed
{
    private readonly ApplicationDbContext _applicationDb;
    private readonly IdentitySeed _identitySeed;

    public Seed(ApplicationDbContext applicationDb, IdentitySeed identitySeed)
    {
        _applicationDb = applicationDb;
        _identitySeed = identitySeed;
    }

    public async Task InitializeDb()
    {
        await _applicationDb.Database.MigrateAsync();
        await _identitySeed.ApplySeed();
    }
}