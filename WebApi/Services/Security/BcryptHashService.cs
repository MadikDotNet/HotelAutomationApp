using HotelAutomationApp.Infrastructure.Interfaces.Security.Models;
using HotelAutomationApp.Infrastructure.Interfaces.Security.Services;

namespace HotelAutomationApp.WebApi.Services.Security;

public class BcryptHashService : IHashService
{
        public Hash GetHash(string target)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(target);
        }

        public bool Verify(Hash hash, string comparable)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(comparable, hash);
        }
}