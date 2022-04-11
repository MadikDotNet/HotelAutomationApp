using AutoMapper;
using HotelAutomationApp.Application.Users.Models;
using HotelAutomationApp.Domain.Models.Identity;

namespace HotelAutomationApp.Application.Users.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserDto>()
            .ReverseMap();
    }
}