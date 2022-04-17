using AutoMapper;
using HotelAutomationApp.Application.Appeals.Models;
using HotelAutomationApp.Domain.Models.Messaging.Appeals;

namespace HotelAutomationApp.Application.Appeals.Mappings;

public class AppealProfile : Profile
{
    public AppealProfile()
    {
        CreateMap<Appeal, AppealDto>()
            .ReverseMap();
    }
}