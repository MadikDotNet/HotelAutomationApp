using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Common.Abstractions;
using HotelAutomationApp.Domain.Models.ValueObjects;

namespace HotelAutomationApp.Domain.Models.Services;

public class Service : BaseDictionary, ITreeDictionary
{
    public string? ParentId { get; set; }
    public bool IsAdditional { get; set; }
    public Price PricePerHour { get; set; }
}