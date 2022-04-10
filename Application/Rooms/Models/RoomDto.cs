using HotelAutomationApp.Application.Common.Models;
using HotelAutomationApp.Application.MediaFiles.Models;

namespace HotelAutomationApp.Application.Rooms.Models
{
    public record RoomDto : AuditableEntityDto
    {
        public string Id { get; set; }
        public int MaxGuestsCount { get; set; }
        public double Capacity { get; set; }
        public decimal PricePerHour { get; set; }
        public bool IsAvailable { get; set; }
        public string RoomGroupId { get; set; }
        public string RoomGroupName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<FileMetadataDto> Images { get; set; }
    }
}