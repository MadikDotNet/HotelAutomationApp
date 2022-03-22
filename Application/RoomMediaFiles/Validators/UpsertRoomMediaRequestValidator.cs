using FluentValidation;
using HotelAutomationApp.Application.Extensions.FluentValidationExtensions;
using HotelAutomationApp.Application.RoomMediaFiles.UseCases;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.RoomMediaFiles.Validators;

public class UpsertRoomMediaRequestValidator : AbstractValidator<UpsertRoomFileRequest>
{
    public UpsertRoomMediaRequestValidator(IApplicationDbContext applicationDb)
    {
        RuleFor(q => q.RoomId).IsExist<UpsertRoomFileRequest, Room>(applicationDb);
    }
}