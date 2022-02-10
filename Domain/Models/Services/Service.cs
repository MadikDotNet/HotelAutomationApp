using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Common.Abstractions;

namespace HotelAutomationApp.Domain.Models.Services;

public class Service : BaseDictionary, ITreeDictionary
{
    public string? ParentId { get; set; }
}