using HotelAutomationApp.Application.Common.Dictionary.Models;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Application.Services.Models;

namespace HotelAutomationApp.Application.RoomGroups.Models;

public record RoomGroupDto : BaseDictionaryDto
{
    public decimal MinPrice { get; set; }
    public string? FileId { get; set; }
    public ICollection<ServiceDto> AvailableServices { get; set; }
}