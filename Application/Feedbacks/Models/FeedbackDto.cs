using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Domain.Common.Abstractions.Audition;

namespace HotelAutomationApp.Application.Feedbacks.Models;

public record FeedbackDto : BaseEntityDto, ICreateAuditor
{
    public string AppealId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreationDate { get; set; }
}