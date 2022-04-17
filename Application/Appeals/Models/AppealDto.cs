using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Domain.Common.Abstractions.Audition;
using HotelAutomationApp.Domain.Models.Messaging;
using HotelAutomationApp.Domain.Models.Messaging.Appeals;

namespace HotelAutomationApp.Application.Appeals.Models;

public record AppealDto : BaseEntityDto, ICreateAuditor
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreationDate { get; set; }
    public AppealStatus Status { get; set; }
    public string? FeedbackId { get; set; }
    public string? FeedbackTitle { get; set; }    
    public string? FeedbackBody { get; set; }    
}