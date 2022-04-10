using HotelAutomationApp.Application.ApplicationServices.Dictionary;
using HotelAutomationApp.Application.Services.Models;
using HotelAutomationApp.Domain.Models.Services;
using HotelAutomationApp.WebApi.Controllers.Common;
using MediatR;

namespace HotelAutomationApp.WebApi.Controllers;

public class ServiceController : DictionaryController
    <Service, ServiceDto, DictionaryCrudService<Service, ServiceDto>>
{
    public ServiceController(DictionaryCrudService<Service, ServiceDto> dictionaryService) : base(dictionaryService)
    {
    }
}