namespace HotelAutomationApp.Domain.Common
{
    /// <summary>
    /// Image representation
    /// </summary>
    public class Image : BaseEntity
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Data { get; set; }
    }
}