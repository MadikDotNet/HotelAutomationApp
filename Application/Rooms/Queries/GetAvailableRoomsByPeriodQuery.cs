using HotelAutomationApp.Domain.Models.Bookings;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Common.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Rooms.Queries;

public class GetAvailableRoomsByPeriodQuery : IRequest<List<Room>>
{
    public GetAvailableRoomsByPeriodQuery(IPeriod? period = null, string? roomId = null)
    {
        RoomId = roomId;
        Period = period;
    }

    public string? RoomId { get; }
    public IPeriod? Period { get; }

    private class Handler : IRequestHandler<GetAvailableRoomsByPeriodQuery, List<Room>>
    {
        private readonly IApplicationDbContext _applicationDb;

        public Handler(IApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }

        public async Task<List<Room>> Handle(GetAvailableRoomsByPeriodQuery request,
            CancellationToken cancellationToken)
        {
            var bookings = from booking in _applicationDb.Booking
                where request.RoomId == null ||
                      booking.RoomId == request.RoomId
                where booking.BookingState == BookingState.Confirmed ||
                      booking.BookingState == BookingState.Ordered
                select booking;

            bookings = from booking in bookings
                where request.Period != null &&
                      (request.Period.DateFrom <= booking.DateFrom &&
                       request.Period.DateTo >= booking.DateTo ||
                       request.Period.DateFrom >= booking.DateFrom &&
                       request.Period.DateFrom <= booking.DateTo ||
                       request.Period.DateTo >= booking.DateFrom &&
                       request.Period.DateTo <= booking.DateTo) ||
                      request.Period == null &&
                      DateTime.UtcNow >= booking.DateFrom &&
                      DateTime.UtcNow <= booking.DateTo
                select booking;

            return await (from room in _applicationDb.Room
                where request.RoomId == null || room.Id == request.RoomId
                join booking in bookings on room.Id equals booking.RoomId into bookingsGp
                from booking in bookingsGp.DefaultIfEmpty()
                where booking == null
                select room).ToListAsync(cancellationToken);
        }
    }
}