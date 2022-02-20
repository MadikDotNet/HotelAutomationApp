using AutoMapper;
using HotelAutomationApp.Application.Common.Dictionary.Models;
using HotelAutomationApp.Application.Services.Models;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.Services;

namespace HotelAutomationApp.Application.Services.Mappings;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Service, ServiceDto>()
            .IncludeBase<BaseDictionary, BaseDictionaryDto>()
            .ReverseMap();
    }
}