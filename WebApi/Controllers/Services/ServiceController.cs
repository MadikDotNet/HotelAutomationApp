using HotelAutomationApp.Application.ApplicationServices.Dictionary;
using HotelAutomationApp.Application.Services.Models;
using HotelAutomationApp.Domain.Models.Services;
using HotelAutomationApp.WebApi.Controllers.Common;

namespace HotelAutomationApp.WebApi.Controllers.Services;

public class ServiceController : TreeDictionaryController
    <Service, ServiceDto, TreeDictionaryCrudService<Service, ServiceDto>>
{
    public ServiceController(TreeDictionaryCrudService<Service, ServiceDto> dictionaryService) : base(dictionaryService)
    {
    }
}