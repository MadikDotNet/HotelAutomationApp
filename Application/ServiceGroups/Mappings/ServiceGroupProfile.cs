using AutoMapper;
using HotelAutomationApp.Application.ServiceGroups.Models;
using HotelAutomationApp.Domain.Models.ServiceGroups;

namespace HotelAutomationApp.Application.ServiceGroups.Mappings;

public class ServiceGroupProfile : Profile
{
    public ServiceGroupProfile()
    {
        CreateMap<ServiceGroup, ServiceGroupDto>()
            .ReverseMap();
    }
}