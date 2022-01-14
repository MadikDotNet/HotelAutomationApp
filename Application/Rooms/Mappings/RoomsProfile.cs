using AutoMapper;
using HotelAutomation.Domain.Models.Rooms;
using HotelAutomationApp.Application.Rooms.Models;
using HotelAutomationApp.Domain.Models.Rooms;

namespace HotelAutomationApp.Application.Rooms.Mappings
{
    public class RoomsProfile : Profile
    {
        public RoomsProfile()
        {
            CreateMap<Room, RoomDto>()
                .ForMember(q => q.PricePerNight, w => w.MapFrom(q => q.PricePerNight.Value));
        }
    }
}