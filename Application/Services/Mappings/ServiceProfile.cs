using AutoMapper;
using HotelAutomationApp.Application.Services.Models;
using HotelAutomationApp.Domain.Models.Services;

namespace HotelAutomationApp.Application.Services.Mappings;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Service, ServiceDto>()
            .ForMember(q => q.Name, w => w.MapFrom(q => q.Name.Value))
            .ReverseMap();
    }
}