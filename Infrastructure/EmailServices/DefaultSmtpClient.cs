using HotelAutomationApp.Infrastructure.Interfaces.EmailServices.Configuration;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using ISmtpClient = HotelAutomationApp.Infrastructure.Interfaces.EmailServices.ISmtpClient;

namespace HotelAutomationApp.Infrastructure.EmailServices;

public class DefaultSmtpClient : ISmtpClient
{
    private readonly SmtpConfiguration _configuration;

    public DefaultSmtpClient(IOptions<SmtpConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public async Task SendMail(string title, string body, string[] toAddresses, bool isMessageHtml = false)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(_configuration.DisplayName, _configuration.Email));
        emailMessage.To.AddRange(toAddresses.Select(q => new MailboxAddress("", q)));
        emailMessage.Subject = title;

        emailMessage.Body = isMessageHtml
            ? new TextPart(TextFormat.Html)
            {
                Text = body
            }
            : new TextPart(TextFormat.Text)
            {
                Text = body
            }; 
        
        using var client = new SmtpClient();

        await client.ConnectAsync(_configuration.Host, _configuration.Port, false);
        await client.AuthenticateAsync(_configuration.Email, _configuration.Password);
        await client.SendAsync(emailMessage);

        await client.DisconnectAsync(true);
    }
}