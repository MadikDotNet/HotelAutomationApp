namespace HotelAutomationApp.Infrastructure.Interfaces.EmailServices.Configuration;

public class SmtpConfiguration
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string DisplayName { get; set; }
}