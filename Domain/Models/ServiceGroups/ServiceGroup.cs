using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Common.Abstractions;
using HotelAutomationApp.Domain.Models.Services;

namespace HotelAutomationApp.Domain.Models.ServiceGroups;

public class ServiceGroup : BaseDictionary, ITreeDictionary
{
    public string? ParentId { get; set; }
    public ICollection<Service> Services { get; set; }
}