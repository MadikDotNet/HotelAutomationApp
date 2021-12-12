using HotelAutomationApp.Shared;

namespace HotelAutomationApp.Application.Auth.Models
{
    public class UserCredentials
    {
        public UserCredentials(string login, string password)
        {
            Login = login.ThrowIfArgNullOrWhiteSpace(nameof(Login));
            Password = password.ThrowIfArgNullOrWhiteSpace(nameof(Password));
        }

        public string Login { get; }
        public string Password { get; }
    }
}