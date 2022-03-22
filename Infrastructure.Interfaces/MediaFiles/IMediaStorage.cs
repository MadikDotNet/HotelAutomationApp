namespace HotelAutomationApp.Infrastructure.Interfaces.MediaFiles;

public interface IMediaStorage
{
    Task<string> UploadAsync(byte[] content);
    Task<byte[]> DownloadAsync(string id);
    Task RemoveAsync(string id);
    Task<bool> IsExistAsync(string id);
}