using AutoMapper;
using HotelAutomationApp.Application.Bookings.Models;
using HotelAutomationApp.Domain.Models.Bookings;

namespace HotelAutomationApp.Application.Bookings.Mappings;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Booking, BookingDto>()
            .ForMember(q => q.Services, w => w.MapFrom(q => q.Services.Select(e => e.Service)));
    }
}