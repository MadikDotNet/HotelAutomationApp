using HotelAutomationApp.Application.Common.Dictionary.Models;

namespace HotelAutomationApp.Application.RoomGroups.Models;

public record RoomGroupDto : BaseDictionaryDto
{
    public decimal MinPrice { get; set; }
}