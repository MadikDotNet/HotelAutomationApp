using HotelAutomationApp.Application.Common.Models;
using HotelAutomationApp.Application.File.Models;

namespace HotelAutomationApp.Application.Rooms.Models
{
    public class RoomDto : AuditableEntityDto
    {
        public string Id { get; set; }
        public int MaxGuestsCount { get; set; }
        public double Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public string RoomGroupId { get; set; }
        public string RoomGroupName { get; set; }
        public ICollection<MediaDto> Images { get; set; }
    }
}