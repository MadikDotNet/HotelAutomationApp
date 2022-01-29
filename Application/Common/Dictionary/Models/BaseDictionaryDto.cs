namespace HotelAutomationApp.Application.Common.Dictionary.Models;

public record BaseDictionaryDto : AuditableEntityDto
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string? ParentId { get; set; }
}