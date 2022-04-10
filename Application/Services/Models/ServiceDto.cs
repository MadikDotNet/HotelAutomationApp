using HotelAutomationApp.Application.Common.Dictionary.Models;

namespace HotelAutomationApp.Application.Services.Models;

public record ServiceDto : BaseDictionaryDto
{
    public decimal PricePerHour { get; set; }
    public string ServiceGroupId { get; set; }
}