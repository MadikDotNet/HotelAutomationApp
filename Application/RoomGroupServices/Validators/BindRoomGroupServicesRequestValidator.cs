using FluentValidation;
using HotelAutomationApp.Application.RoomGroupServices.UseCases;
using HotelAutomationApp.Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.RoomGroupServices.Validators;

public class BindRoomGroupServicesRequestValidator : AbstractValidator<BindRoomGroupServicesRequest>
{
    public BindRoomGroupServicesRequestValidator(IApplicationDbContext applicationDb)
    {
        RuleFor(q => q.RoomGroupId)
            .MustAsync(
                async (rgId, token) => await applicationDb.RoomGroup.FindAsync(new object[] {rgId}, token) is { })
            .WithMessage("Room group not found");
    }
}