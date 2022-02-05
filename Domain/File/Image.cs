using HotelAutomationApp.Domain.Common;

namespace HotelAutomationApp.Domain.File
{
    /// <summary>
    /// Image representation
    /// </summary>
    public class Image : BaseEntity
    {
        public Image()
        {
            
        }

        public Image(string fileName, string fileType, string data)
        {
            FileName = fileName;
            FileType = fileType;
            Data = data;
        }

        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Data { get; set; }
    }
}