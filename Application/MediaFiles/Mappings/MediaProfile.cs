using AutoMapper;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Domain.Models.MediaFiles;

namespace HotelAutomationApp.Application.MediaFiles.Mappings
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<Media, MediaDto>().ReverseMap();
        }
    }
}