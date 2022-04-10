using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Domain.Models.Bookings;
using HotelAutomationApp.Domain.Models.BookingServices;
using HotelAutomationApp.Domain.Models.Services;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Bookings.Commands;

public class CreateBookingCommand : UTCPeriod, IRequest
{
    public CreateBookingCommand(string clientId, string roomId, string[]? serviceIds, DateTime dateFrom,
        DateTime dateTo) : base(dateFrom, dateTo)
    {
        ClientId = clientId;
        RoomId = roomId;
        ServiceIds = serviceIds;
    }

    public string ClientId { get; }
    public string RoomId { get; }
    public string[]? ServiceIds { get; }

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
                .ThenInclude(q => q.Service)
                .FirstAsync(q => q.Id == request.RoomId, cancellationToken);

            var totalPeriod = (decimal) (request.DateTo - request.DateFrom).TotalHours;

            decimal servicesCost = 0;
            var services = room.RoomGroup.RoomGroupServices.Select(q => q.Service).ToList();

            if (request.ServiceIds is { })
            {
                var additionalServices = (await _applicationDb.Service
                    .Where(service => request.ServiceIds.Contains(service.Id))
                    .ToListAsync(cancellationToken))
                    .Except(services)
                    .ToList();

                if (additionalServices.Any(q => !q.IsAdditional))
                {
                    throw new ApplicationException("Specified services contains not additional service");
                }
                
                servicesCost = additionalServices
                    .Where(service => services.All(rgService => rgService.Id != service.Id))
                    .Sum(q => q.PricePerHour * totalPeriod);

                services = services.Concat(additionalServices).ToList();
            }

            var totalCost = room.PricePerHour * totalPeriod + servicesCost;

            var newBooking = new Booking
            {
                ClientId = request.ClientId,
                RoomId = request.RoomId,
                TotalPrice = totalCost,
                DateFrom = request.DateFrom.ToUniversalTime(),
                DateTo = request.DateTo.ToUniversalTime(),
                BookingState = BookingState.Ordered,
                CreatedBy = request.ClientId,
                LastModifiedBy = request.ClientId,
            };

            var newBookingServices = services.Select(service => new BookingService
            {
                Id = Guid.NewGuid().ToString(),
                BookingId = newBooking.Id,
                ServiceId = service.Id
            }).ToList();

            newBooking.Services = newBookingServices;

            _applicationDb.Booking.Add(newBooking);

            await _applicationDb.SaveChangesAsync(CancellationToken.None);
        }
    }
}