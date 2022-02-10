namespace HotelAutomationApp.Infrastructure.Interfaces.Auth.Exceptions;

public class UserBlockedException : Exception
{
    public UserBlockedException() : base("User is blocked")
    {
        
    }
}