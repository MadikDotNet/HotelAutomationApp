using System.Threading;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmailHubController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> SendEmail(
            string toEmailAddress,
            string emailSubject,
            string emailMessage,
            CancellationToken cancellationToken)
        {
            using var message = new MimeMessage();
            
            message.From.Add(new MailboxAddress("Bank account", "bank-account@gmail.com"));
            message.To.Add(new MailboxAddress("", toEmailAddress));
            message.Subject = emailSubject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailMessage
            };
            
            using var smtpClient = new SmtpClient();

            await smtpClient.ConnectAsync("smtp.gmail.com", 465, true, cancellationToken);
            await smtpClient.AuthenticateAsync("moldageldinmadik@gmail.com", "fsokeskerjckwptx", cancellationToken);
            await smtpClient.SendAsync(message, cancellationToken);

            return Ok();
        }
    }
}