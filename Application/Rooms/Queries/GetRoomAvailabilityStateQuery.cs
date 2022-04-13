using HotelAutomationApp.Domain.Models.Bookings;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Common.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Rooms.Queries;

public class GetRoomAvailabilityStateQuery : IRequest<bool>
{
    public GetRoomAvailabilityStateQuery(string roomId, IPeriod? period = null)
    {
        RoomId = roomId;
        Period = period;
    }

    public string RoomId { get; }
    public IPeriod? Period { get; }

    private class Handler : IRequestHandler<GetRoomAvailabilityStateQuery, bool>
    {
        private readonly IApplicationDbContext _applicationDb;

        public Handler(IApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }

        public async Task<bool> Handle(GetRoomAvailabilityStateQuery request, CancellationToken cancellationToken) =>
            !await _applicationDb.Booking.Where(booking => booking.RoomId == request.RoomId)
                .Where(booking => booking.BookingState == BookingState.Confirmed ||
                                  booking.BookingState == BookingState.Ordered &&
                                  request.Period != null &&
                                  (request.Period.DateFrom <= booking.DateFrom &&
                                   request.Period.DateTo >= booking.DateTo ||
                                   request.Period.DateFrom >= booking.DateFrom &&
                                   request.Period.DateFrom <= booking.DateTo ||
                                   request.Period.DateTo >= booking.DateFrom &&
                                   request.Period.DateTo <= booking.DateTo) ||
                                  request.Period == null &&
                                  DateTime.UtcNow >= booking.DateFrom &&
                                  DateTime.UtcNow <= booking.DateTo).AnyAsync(cancellationToken);
    }
}