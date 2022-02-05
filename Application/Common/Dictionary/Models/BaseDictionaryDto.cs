namespace HotelAutomationApp.Application.Common.Dictionary.Models;

public record BaseDictionaryDto : BaseEntityDto
{
    private string? _parentId;
    
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }

    public string? ParentId
    {
        get => _parentId;
        set => _parentId = string.IsNullOrEmpty(value) || !Guid.TryParse(value, out _) ? null : value;
    }
}