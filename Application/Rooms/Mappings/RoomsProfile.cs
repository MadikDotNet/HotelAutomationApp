using AutoMapper;
using HotelAutomationApp.Application.Rooms.Models;
using HotelAutomationApp.Domain.Models.Rooms;

namespace HotelAutomationApp.Application.Rooms.Mappings
{
    public class RoomsProfile : Profile
    {
        public RoomsProfile()
        {
            CreateMap<Room, RoomDto>()
                .ForMember(q => q.PricePerNight, w => w.MapFrom(q => q.PricePerNight.Value))
                .ForMember(q => q.RoomGroupName, w => w.MapFrom(q => q.RoomGroup.Name.Value))
                .ForMember(q => q.Images, w => w.MapFrom(q => q.RoomFiles.Select(roomFile => roomFile.FileMetadata)));
        }
    }
}