using HotelAutomationApp.Application.ApplicationServices.Dictionary;
using HotelAutomationApp.Application.ServiceGroups.Models;
using HotelAutomationApp.Domain.Models.ServiceGroups;
using HotelAutomationApp.WebApi.Controllers.Common;

namespace HotelAutomationApp.WebApi.Controllers;

public class ServiceGroupController : TreeDictionaryController
    <ServiceGroup, ServiceGroupDto, TreeDictionaryCrudService<ServiceGroup, ServiceGroupDto>>
{
    public ServiceGroupController(TreeDictionaryCrudService<ServiceGroup, ServiceGroupDto> dictionaryService) : base(dictionaryService)
    {
    }
}