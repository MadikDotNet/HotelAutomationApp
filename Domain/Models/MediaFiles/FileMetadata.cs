using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Shared.Extensions;

namespace HotelAutomationApp.Domain.Models.MediaFiles
{
    public class FileMetadata : AuditableEntity
    {
        public FileMetadata()
        {
        }

        public FileMetadata(string id, string fileName, string fileType)
        {
            Id = id;
            FileName = fileName.EnsureIsNotEmpty();
            FileType = fileType.EnsureIsNotEmpty();
        }

        public string FileName { get; set; }
        public string FileType { get; set; }
    }
}