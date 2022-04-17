using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Common.Abstractions.Audition;
using HotelAutomationApp.Domain.Models.Identity;

namespace HotelAutomationApp.Domain.Models.Messaging.Appeals;

public class Appeal : BaseEntity, ICreateAuditor
{
    public Appeal()
    {
    }

    public Appeal(
        string email,
        string userName,
        string title,
        string body,
        DateTime creationDate,
        AppealStatus status)
    {
        Email = email;
        UserName = userName;
        Title = title;
        Body = body;
        CreationDate = creationDate;
        Status = status;
    }

    public string Email { get; set; }
    public string UserName { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    [ForeignKey(nameof(User))]
    public string? CreatedBy { get; set; }
    public ApplicationUser User { get; set; }
    public DateTime CreationDate { get; set; }
    public AppealStatus Status { get; set; }
    [ForeignKey(nameof(Feedback))]
    public string? FeedbackId { get; set; }
    public Feedback Feedback { get; set; }    

    public static Appeal New(string email, string username,string title, string body)
    {
        return new Appeal(email, username, title, body, DateTime.UtcNow, AppealStatus.Written);
    }
}