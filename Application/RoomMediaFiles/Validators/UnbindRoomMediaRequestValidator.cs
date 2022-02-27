using FluentValidation;
using HotelAutomationApp.Application.Extensions.FluentValidationExtensions;
using HotelAutomationApp.Application.RoomMediaFiles.UseCases;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.RoomMediaFiles.Validators;

public class UnbindRoomMediaRequestValidator : AbstractValidator<UnbindRoomMediaRequest>
{
    public UnbindRoomMediaRequestValidator(IApplicationDbContext applicationDb)
    {
        RuleFor(q => q.RoomId).IsExist<UnbindRoomMediaRequest, Room>(applicationDb);
    }
}