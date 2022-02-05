using System.Threading.Tasks;
using HotelAutomationApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.WebApi.Seeds;

public class Seed
{
    private readonly ApplicationDbContext _db;
    private readonly IdentitySeed _identitySeed;

    public Seed(ApplicationDbContext db, IdentitySeed identitySeed)
    {
        _db = db;
        _identitySeed = identitySeed;
    }

    public async Task InitializeDb()
    {
        await _db.Database.MigrateAsync();
        await _identitySeed.ApplySeed();
    }
}