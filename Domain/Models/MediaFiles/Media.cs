using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Shared.Extensions;

namespace HotelAutomationApp.Domain.Models.MediaFiles
{
    /// <summary>
    /// Media representation
    /// </summary>
    public class Media : BaseEntity
    {
        public Media()
        {
        }

        public Media(string id, string fileName, string fileType, string data) : base(id)
        {
            FileName = fileName.EnsureIsNotEmpty();
            FileType = fileType.EnsureIsNotEmpty();
            Data = data.EnsureIsNotEmpty();
        }

        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Data { get; set; }
    }
}