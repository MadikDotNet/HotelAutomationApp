using AutoMapper;
using HotelAutomationApp.Application.Common.Dictionary.Models;
using HotelAutomationApp.Domain.Common;

namespace HotelAutomationApp.Application.Common.Mappings;

public class BaseDictionaryProfile : Profile
{
    public BaseDictionaryProfile()
    {
        CreateMap<BaseDictionary, BaseDictionaryDto>()
            .ReverseMap();
    }
}