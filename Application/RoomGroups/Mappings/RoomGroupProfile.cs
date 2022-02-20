using AutoMapper;
using HotelAutomationApp.Application.RoomGroups.Models;
using HotelAutomationApp.Domain.Models.RoomGroups;

namespace HotelAutomationApp.Application.RoomGroups.Mappings;

public class RoomGroupProfile : Profile
{
    public RoomGroupProfile()
    {
        CreateMap<RoomGroup, RoomGroupDto>()
            .ForMember(q => q.Name, w => w.MapFrom(q => q.Name.Value))
            .ForMember(q => q.MinPrice, w => w.MapFrom(q => q.MinPrice.Value))
            .ReverseMap();

        CreateMap<RoomGroup, RoomGroupWithAvailableServicesDto>()
            .IncludeBase<RoomGroup, RoomGroupDto>()
            .ForMember(q => q.AvailableServices, w => w.MapFrom(q => q.RoomGroupServices.Select(e => e.Service)));
    }
}