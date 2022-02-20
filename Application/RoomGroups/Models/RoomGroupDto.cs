using HotelAutomationApp.Application.Common.Dictionary.Models;
using HotelAutomationApp.Application.MediaFiles.Models;

namespace HotelAutomationApp.Application.RoomGroups.Models;

public record RoomGroupDto : BaseDictionaryDto
{
    public decimal MinPrice { get; set; }
    public string MediaId { get; set; }
    public MediaDto? Media { get; set; }
}