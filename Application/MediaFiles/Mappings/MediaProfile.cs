using AutoMapper;
using HotelAutomationApp.Application.File.Models;
using HotelAutomationApp.Domain.MediaFiles;

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