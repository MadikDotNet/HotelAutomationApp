using System.Threading;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using HotelAutomationApp.Application.EmailHub;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmailHubController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> SendEmail(
            [FromBody]SendMailRequest request,
            CancellationToken cancellationToken)
        {
            using var message = new MimeMessage();
            
            message.From.Add(new MailboxAddress("Bank account", "bank-account@gmail.com"));
            message.To.Add(new MailboxAddress("", request.ToEmailAddress));
            message.Subject = request.EmailSubject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = request.EmailMessage
            };
            
            using var smtpClient = new SmtpClient();

            await smtpClient.ConnectAsync("smtp.gmail.com", 465, true, cancellationToken);
            await smtpClient.AuthenticateAsync("uranus77715@gmail.com", "testpswd", cancellationToken);
            await smtpClient.SendAsync(message, cancellationToken);

            return Ok();
        }
    }
}