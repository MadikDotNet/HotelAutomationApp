using System;

namespace HotelAutomationApp.Infrastructure.Interfaces.Auth.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Invalid login or password")
        {
            
        }
    }
}