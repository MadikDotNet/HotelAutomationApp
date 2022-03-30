using FluentValidation;
using HotelAutomationApp.Application.Bookings.UseCases;
using HotelAutomationApp.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Bookings.Validators;

public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
{
    public CreateBookingRequestValidator(IApplicationDbContext applicationDb)
    {
        RuleFor(q => q.ClientId)
            .MustAsync(async (clientId, token) => await applicationDb.User.AnyAsync(q => q.Id == clientId, token))
            .WithMessage("Client not found");

        RuleFor(q => q.Roomid)
            .MustAsync(async (roomId, token) => await applicationDb.Room.AnyAsync(q => q.Id == roomId, token))
            .WithMessage("Room not found");
    }
}