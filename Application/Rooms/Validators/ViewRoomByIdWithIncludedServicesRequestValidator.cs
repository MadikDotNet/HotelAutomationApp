using FluentValidation;
using HotelAutomationApp.Application.Rooms.UseCases;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Common;

namespace HotelAutomationApp.Application.Rooms.Validators;

public class ViewRoomByIdWithIncludedServicesRequestValidator :
    AbstractValidator<ViewRoomByIdWithIncludedServicesRequest>
{
    public ViewRoomByIdWithIncludedServicesRequestValidator(IApplicationDbContext applicationDb)
    {
        RuleFor(q => q.RoomId)
            .MustAsync(async (id, token) => await applicationDb.Room.FindAsync(id.YieldObjectArray(), token)
                is { })
            .WithMessage("Room not found");
    }
}