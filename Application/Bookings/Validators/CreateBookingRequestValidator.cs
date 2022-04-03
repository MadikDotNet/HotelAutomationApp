using FluentValidation;
using HotelAutomationApp.Application.Bookings.UseCases;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Bookings.Validators;

public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
{
    public CreateBookingRequestValidator(IApplicationDbContext applicationDb, ISecurityContext securityContext)
    {
        RuleFor(_ => _)
            .MustAsync(async (_, token) =>
                securityContext.UserExists && await applicationDb.User.AnyAsync(q => q.Id == securityContext.UserId,
                    token))
            .WithMessage("Client not found");

        RuleFor(q => q.Roomid)
            .MustAsync(async (roomId, token) => await applicationDb.Room.AnyAsync(q => q.Id == roomId, token))
            .WithMessage("Room not found");

        RuleFor(q => q.Roomid)
            .MustAsync(async (roomId, token) =>
                await applicationDb.Room.FindAsync(roomId.YieldObjectArray(), token) is {IsAvailable: true})
            .WithMessage("Room is not available at this moment");
    }
}