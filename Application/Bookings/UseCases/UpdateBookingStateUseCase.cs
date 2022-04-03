using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Domain.Models.Bookings;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Common;
using MediatR;

namespace HotelAutomationApp.Application.Bookings.UseCases;

public class UpdateBookingStateUseCase : UseCase<UpdateBookingStateRequest>
{
    private readonly IApplicationDbContext _applicationDb;

    public UpdateBookingStateUseCase(IApplicationDbContext applicationDb)
    {
        _applicationDb = applicationDb;
    }

    protected override async Task HandleRequestAsync(
        UpdateBookingStateRequest request,
        CancellationToken cancellationToken)
    {
        if (request.BookingState is BookingState.Expired)
        {
            throw new ApplicationException("State cannot be settled as expired");
        }

        var booking = await _applicationDb.Booking.FindAsync(request.BookingId.YieldObjectArray(), cancellationToken);

        if (booking!.BookingState == request.BookingState)
        {
            throw new ApplicationException($"Booking state already is {booking.BookingState}");
        }

        if (booking.BookingState is not BookingState.Ordered)
        {
            throw new ApplicationException($"State can updated once");
        }

        booking.BookingState = request.BookingState;
        await _applicationDb.SaveChangesAsync(CancellationToken.None);
    }
}

public class UpdateBookingStateRequest : IRequest
{
    public string BookingId { get; set; }
    public BookingState BookingState { get; set; }
}