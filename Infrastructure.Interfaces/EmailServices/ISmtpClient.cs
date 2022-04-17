namespace HotelAutomationApp.Infrastructure.Interfaces.EmailServices;

public interface ISmtpClient
{
    Task SendMail(string title, string body, string[] toAddresses, bool isMessageHtml = false);
}