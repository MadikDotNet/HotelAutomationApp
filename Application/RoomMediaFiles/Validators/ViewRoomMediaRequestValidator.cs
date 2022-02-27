using FluentValidation;
using HotelAutomationApp.Application.Extensions.FluentValidationExtensions;
using HotelAutomationApp.Application.RoomMediaFiles.UseCases;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.RoomMediaFiles.Validators;

public class ViewRoomMediaRequestValidator : AbstractValidator<ViewRoomMediaRequest>
{
    public ViewRoomMediaRequestValidator(IApplicationDbContext applicationDb)
    {
        RuleFor(q => q.RoomId).IsExist<ViewRoomMediaRequest, Room>(applicationDb);
    }
}