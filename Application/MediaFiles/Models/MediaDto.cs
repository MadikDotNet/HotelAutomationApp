using HotelAutomationApp.Application.Common;

namespace HotelAutomationApp.Application.MediaFiles.Models
{
    public record MediaDto : BaseEntityDto
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Data { get; set; }
    }
}