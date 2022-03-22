using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HotelAutomationApp.Infrastructure.Interfaces.MediaFiles;
using Microsoft.AspNetCore.Hosting;

namespace HotelAutomationApp.WebApi.Services.MediaFiles;

public class InServerMediaStorage : IMediaStorage
{
    private readonly string _rootPath;

    public InServerMediaStorage(IWebHostEnvironment webHostEnvironment)
    {
        _rootPath = webHostEnvironment.WebRootPath + "/Files/";

        if (!Directory.Exists(_rootPath))
        {
            Directory.CreateDirectory(_rootPath);
        }
    }

    public async Task<string> UploadAsync(byte[] content)
    {
        var id = Guid.NewGuid().ToString();

        await using var fileStream = File.Create(_rootPath + id);

        await fileStream.WriteAsync(content);

        return id;
    }

    public async Task<byte[]> DownloadAsync(string id)
    {
        var path = _rootPath + id;

        await EnsurePathIsExists(path);
        
        return await File.ReadAllBytesAsync(path);
    }

    public async Task RemoveAsync(string id)
    {
        var path = _rootPath + id;

        await EnsurePathIsExists(path);

        File.Delete(path);
    }

    public Task<bool> IsExistAsync(string id) => Task.FromResult(File.Exists(_rootPath + id));

    private async Task EnsurePathIsExists(string path)
    {
        if (await IsExistAsync(path))
        {
            throw new InvalidOperationException("Cannot find file");
        }
    }
}