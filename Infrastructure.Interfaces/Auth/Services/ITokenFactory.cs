using HotelAutomationApp.Domain.Models.Identity;

namespace HotelAutomationApp.Infrastructure.Interfaces.Auth.Services
{
    public interface ITokenFactory
    {
        string CreateToken(User user);
    }
}