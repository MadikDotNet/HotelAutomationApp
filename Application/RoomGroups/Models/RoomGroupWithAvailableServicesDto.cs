using HotelAutomationApp.Application.Services.Models;

namespace HotelAutomationApp.Application.RoomGroups.Models;

public record RoomGroupWithAvailableServicesDto : RoomGroupDto
{
    public ICollection<ServiceDto>? AvailableServices { get; set; }
}