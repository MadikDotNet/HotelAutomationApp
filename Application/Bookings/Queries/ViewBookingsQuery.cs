using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Bookings.Models;
using HotelAutomationApp.Domain.Models.Bookings;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Bookings.Queries;

public class ViewBookingsQuery : IRequest<BookingDto[]>
{
    public ViewBookingsQuery(
        string? clientName,
        string? clientEmail,
        string? roomName,
        BookingState? bookingState,
        decimal? minTotalPrice,
        decimal? maxTotalPrice,
        DateTime? dateFrom,
        DateTime? dateTo,
        string? roomDescription,
        string? roomGroupName,
        string? roomGroupDescription)
    {
        ClientName = clientName;
        ClientEmail = clientEmail;
        RoomName = roomName;
        BookingState = bookingState;
        MinTotalPrice = minTotalPrice;
        MaxTotalPrice = maxTotalPrice;
        DateFrom = dateFrom;
        DateTo = dateTo;
        RoomDescription = roomDescription;
        RoomGroupName = roomGroupName;
        RoomGroupDescription = roomGroupDescription;
    }

    public string? ClientName { get; }
    public string? ClientEmail { get; }
    public string? RoomName { get; }
    public BookingState? BookingState { get; }
    public decimal? MinTotalPrice { get; }
    public decimal? MaxTotalPrice { get; }
    public DateTime? DateFrom { get; }
    public DateTime? DateTo { get; }
    public string? RoomDescription { get; }
    public string? RoomGroupName { get; }
    public string? RoomGroupDescription { get; }

    private class Handler : IRequestHandler<ViewBookingsQuery, BookingDto[]>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDb, IMapper mapper)
        {
            _applicationDb = applicationDb;
            _mapper = mapper;
        }

        public async Task<BookingDto[]> Handle(ViewBookingsQuery request, CancellationToken cancellationToken)
        {
            var bookings = _applicationDb.Booking.AsNoTracking()
                .Where(q => !request.BookingState.HasValue || q.BookingState == request.BookingState);

            return await (from booking in bookings
                    where request.ClientName == null || booking.Client.UserName.Contains(request.ClientName)
                    where request.ClientEmail == null || booking.Client.Email.Contains(request.ClientEmail)
                    where request.RoomName == null || booking.Client.Email.Contains(request.RoomName)
                    where request.RoomDescription == null || booking.Client.Email.Contains(request.RoomDescription)
                    where request.RoomGroupName == null || booking.Client.Email.Contains(request.RoomGroupName)
                    where request.RoomGroupDescription == null ||
                          booking.Client.Email.Contains(request.RoomGroupDescription)
                    where !request.MinTotalPrice.HasValue || booking.TotalPrice > request.MinTotalPrice
                    where !request.MaxTotalPrice.HasValue || booking.TotalPrice < request.MaxTotalPrice
                    where !request.DateFrom.HasValue || booking.DateFrom > request.DateFrom
                    where !request.DateTo.HasValue || booking.DateTo < request.DateTo
                    select booking)
                .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);
        }
    }
}