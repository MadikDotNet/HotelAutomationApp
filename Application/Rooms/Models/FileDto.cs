using Microsoft.AspNetCore.Http;

namespace HotelAutomationApp.Application.Rooms.Models;

public class FileDto
{
    public string? Id { get; set; }
    public IFormFile? File { get; set; }
}