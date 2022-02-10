using HotelAutomationApp.Application.ApplicationServices.Dictionary;
using HotelAutomationApp.Application.Services.Models;
using HotelAutomationApp.Domain.Models.Services;
using HotelAutomationApp.WebApi.Controllers.Common;

namespace HotelAutomationApp.WebApi.Controllers.Services;

public class ServiceController : DictionaryTreeController
    <Service, ServiceDto, DictionaryCrudService<Service, ServiceDto>>
{
    public ServiceController(DictionaryCrudService<Service, ServiceDto> dictionaryService) : base(dictionaryService)
    {
    }
}