namespace HotelAutomationApp.Application.EmailHub
{
    public class SendMailRequest
    {
        public string ToEmailAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
    }
}