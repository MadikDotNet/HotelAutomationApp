using HotelAutomationApp.Infrastructure.Interfaces.Security.Models;

namespace HotelAutomationApp.Infrastructure.Interfaces.Security.Services;

public interface IHashService
{
    public Hash GetHash(string target);
    public bool Verify(Hash hash, string comparable);
}