using FluentValidation;
using HotelAutomationApp.Application.Extensions.FluentValidationExtensions;
using HotelAutomationApp.Application.RoomMedia.UseCases;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.RoomMedia.Validators;

public class UpsertRoomMediaRequestValidator : AbstractValidator<UpsertRoomMediaRequest>
{
    public UpsertRoomMediaRequestValidator(IApplicationDbContext applicationDb)
    {
        RuleFor(q => q.RoomId).IsExist<UpsertRoomMediaRequest, Room>(applicationDb);
    }
}