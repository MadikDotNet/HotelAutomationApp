using AutoMapper;
using HotelAutomationApp.Application.Feedbacks.Models;
using HotelAutomationApp.Domain.Models.Messaging;

namespace HotelAutomationApp.Application.Feedbacks.Mappings;

public class FeedbackProfile : Profile
{
    public FeedbackProfile()
    {
        CreateMap<Feedback, FeedbackDto>()
            .ReverseMap();
    }
}