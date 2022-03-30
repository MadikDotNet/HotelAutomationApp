using HotelAutomationApp.Application.Common.Dictionary.Models;
using HotelAutomationApp.Domain.Models.ValueObjects;

namespace HotelAutomationApp.Application.Services.Models;

public record ServiceDto : TreeDictionaryDto<ServiceDto>
{
    public bool IsAdditional { get; set; }
    public Price PricePerHour { get; set; }
}