using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Common.Abstractions.Audition;
using HotelAutomationApp.Domain.Models.Messaging.Appeals;

namespace HotelAutomationApp.Domain.Models.Messaging;

public class Feedback : BaseEntity, ICreateAuditor
{
    public Feedback()
    {
    }

    public Feedback(
        string appealId,
        string title,
        string body,
        string createdBy,
        DateTime creationDate)
    {
        AppealId = appealId;
        Title = title;
        Body = body;
        CreatedBy = createdBy;
        CreationDate = creationDate;
    }

    [ForeignKey(nameof(Appeal))] public string AppealId { get; set; }
    public Appeal Appeal { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreationDate { get; set; }
    

    public static Feedback New(string appealId, string title, string body, string createdBy)
    {
        return new Feedback(appealId, title, body, createdBy, DateTime.UtcNow);
    }
}