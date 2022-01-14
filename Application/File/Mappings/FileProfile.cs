using AutoMapper;
using HotelAutomationApp.Application.File.Models;
using HotelAutomationApp.Domain.File;

namespace HotelAutomationApp.Application.File.Mappings
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<Image, ImageDto>().ReverseMap();
        }
    }
}