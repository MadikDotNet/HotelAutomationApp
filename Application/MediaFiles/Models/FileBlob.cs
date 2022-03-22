namespace HotelAutomationApp.Application.MediaFiles.Models;

public class FileBlob
{
    public FileMetadataDto FileMetadata { get; set; }
    public byte[] Content { get; set; }
}