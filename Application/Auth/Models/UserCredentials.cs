using HotelAutomationApp.Shared.Extensions;

namespace HotelAutomationApp.Application.Auth.Models
{
    public class UserCredentials
    {
        public UserCredentials(string login, string password)
        {
            Login = login.EnsureIsNotEmpty(nameof(Login));
            Password = password.EnsureIsNotEmpty(nameof(Password));
        }

        public string? Login { get; }
        public string? Password { get; }
    }
}