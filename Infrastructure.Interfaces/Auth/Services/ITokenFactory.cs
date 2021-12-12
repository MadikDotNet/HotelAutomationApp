namespace HotelAutomationApp.Infrastructure.Interfaces.Auth.Services
{
    public interface ITokenFactory
    {
        string CreateToken(User user);
    }
}