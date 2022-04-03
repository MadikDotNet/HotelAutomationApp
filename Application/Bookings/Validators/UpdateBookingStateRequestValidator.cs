using FluentValidation;
using HotelAutomationApp.Application.Bookings.UseCases;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Common;

namespace HotelAutomationApp.Application.Bookings.Validators;

public class UpdateBookingStateRequestValidator : AbstractValidator<UpdateBookingStateRequest>
{
    public UpdateBookingStateRequestValidator(IApplicationDbContext applicationDb)
    {
        RuleFor(q => q.BookingId)
            .MustAsync(async (id, token) => await applicationDb.Booking.FindAsync(id.YieldObjectArray(), token) is { })
            .WithMessage("Booking not found");
    }
}