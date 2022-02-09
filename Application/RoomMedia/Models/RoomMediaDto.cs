using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.File.Models;
using HotelAutomationApp.Application.Rooms.Models;

namespace HotelAutomationApp.Application.RoomMedia.Models;

public record RoomMediaDto : BaseEntityDto
{
    public string RoomId { get; set; }
    public RoomDto Room { get; set; }
    public string MediaId { get; }
    public MediaDto Media { get; set; }
}