using AutoMapper;
using HotelAutomationApp.Application.RoomGroups.Models;
using HotelAutomationApp.Domain.Models.RoomGroups;

namespace HotelAutomationApp.Application.RoomGroups.Mappings;

public class RoomGroupProfile : Profile
{
    public RoomGroupProfile()
    {
        CreateMap<RoomGroup, RoomGroupDto>()
            .ForMember(q => q.FileId, w => w.MapFrom(q => q.FileMetadataId))
            .ForMember(q => q.AvailableServices, w => w.MapFrom(q => q.RoomGroupServices.Select(e => e.Service)))
            .ReverseMap();
    }
}