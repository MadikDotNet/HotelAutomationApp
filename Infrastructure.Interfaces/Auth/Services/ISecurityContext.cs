namespace HotelAutomationApp.Infrastructure.Interfaces.Auth.Services
{
    public interface ISecurityContext
    {
        bool UserExists { get;}
        
        string UserId { get; }
    }
}