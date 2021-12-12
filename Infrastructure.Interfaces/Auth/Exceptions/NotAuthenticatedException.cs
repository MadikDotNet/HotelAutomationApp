using System;

namespace HotelAutomationApp.Infrastructure.Interfaces.Auth.Exceptions
{
    public class NotAuthenticatedException : Exception
    {
        public NotAuthenticatedException() : base("User us not authenticated")
        {
            
        }
    }
}