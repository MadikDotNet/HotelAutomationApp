using FluentValidation;
using HotelAutomationApp.Application.Extensions.FluentValidationExtensions;
using HotelAutomationApp.Application.RoomMedia.UseCases;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.RoomMedia.Validators;

public class UnbindRoomMediaRequestValidator : AbstractValidator<UnbindRoomMediaRequest>
{
    public UnbindRoomMediaRequestValidator(IApplicationDbContext applicationDb)
    {
        RuleFor(q => q.RoomId).IsExist<UnbindRoomMediaRequest, Room>(applicationDb);
    }
}