using HotelAutomationApp.Application.Common;

namespace HotelAutomationApp.Application.MediaFiles.Models
{
    public record FileMetadataDto : BaseEntityDto
    {
        public string Id { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
    }
}