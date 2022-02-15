namespace HotelAutomationApp.Application.Common.Dictionary.Models;

public record BaseDictionaryDto : BaseEntityDto
{
    private string? _parentId;
    
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
}