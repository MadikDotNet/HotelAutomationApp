using HotelAutomationApp.Application.Common.Dictionary.Models;
using HotelAutomationApp.Application.Services.Models;

namespace HotelAutomationApp.Application.ServiceGroups.Models;

public record ServiceGroupDto : TreeDictionaryDto<ServiceGroupDto>
{
    public ICollection<ServiceDto> Services { get; set; }
}