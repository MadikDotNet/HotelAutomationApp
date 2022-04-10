using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Common.Abstractions;

namespace HotelAutomationApp.Domain.Models.ServiceGroups;

public class ServiceGroup : BaseDictionary, ITreeDictionary
{
    public string? ParentId { get; set; }
}