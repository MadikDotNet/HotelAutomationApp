using AutoMapper;
using HotelAutomationApp.Application.RoomGroups.Models;
using HotelAutomationApp.Domain.Models.Rooms;

namespace HotelAutomationApp.Application.RoomGroups.Mappings;

public class RoomGroupProfile : Profile
{
    public RoomGroupProfile()
    {
        CreateMap<RoomGroup, RoomGroupDto>()
            .ForMember(q => q.MinPrice, w => w.MapFrom(q => q.MinPrice.Value))
            .ReverseMap();
    }
}