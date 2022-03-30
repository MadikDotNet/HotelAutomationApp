using HotelAutomationApp.Domain.Models.Bookings;
using HotelAutomationApp.Domain.Models.BookingServices;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Common.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Bookings.Commands;

public class CreateBookingCommand : IRequest
{
    public CreateBookingCommand(string clientId, string roomId, string[] serviceIds, IPeriod period)
    {
        ClientId = clientId;
        RoomId = roomId;
        ServiceIds = serviceIds;
        Period = period;
    }

    public string ClientId { get; }
    public string RoomId { get; }
    public string[] ServiceIds { get; }
    public IPeriod Period { get; }

    private class Handler : AsyncRequestHandler<CreateBookingCommand>
    {
        private readonly IApplicationDbContext _applicationDb;

        public Handler(IApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }

        protected override async Task Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var room = await _applicationDb.Room
                .Include(q => q.RoomGroup)
                .ThenInclude(q => q.RoomGroupServices)
                .FirstAsync(q => q.Id == request.RoomId, cancellationToken);

            var services = await _applicationDb.Service.Where(service => request.ServiceIds.Contains(service.Id))
                .ToListAsync(cancellationToken);

            var totalCostForServices = services
                .Where(service => room.RoomGroup.RoomGroupServices.Any(rgService => rgService.Id == service.Id))
                .Sum(q => q.PricePerHour.Value *
                          (decimal) (request.Period.DateTo - request.Period.DateFrom).TotalHours);

            var newBooking = new Booking
            {
                ClientId = request.ClientId,
                RoomId = request.RoomId,
                TotalPrice = room.PricePerHour * (decimal) (request.Period.DateTo - request.Period.DateFrom).TotalHours
                             + totalCostForServices,
                DateFrom = request.Period.DateFrom,
                DateTo = request.Period.DateTo,
            };

            _applicationDb.Booking.Add(newBooking);
            await _applicationDb.SaveChangesAsync(CancellationToken.None);

            var newBookingServices = services.Select(service => new BookingService
            {
                Id = Guid.NewGuid().ToString(),
                BookingId = newBooking.Id,
                ServiceId = service.Id
            });

            _applicationDb.BookingService.AddRange(newBookingServices);
            await _applicationDb.SaveChangesAsync(CancellationToken.None);
        }
    }
}