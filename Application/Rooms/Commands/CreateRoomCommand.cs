using HotelAutomationApp.Application.File.Models;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;

namespace HotelAutomationApp.Application.Rooms.Commands
{
    public class CreateRoomCommand : IRequest
    {
        public CreateRoomCommand(
            int maxGuestsCount,
            double capacity,
            decimal pricePerNight,
            string roomGroupId,
            ICollection<MediaDto> images)
        {
            MaxGuestsCount = maxGuestsCount;
            Capacity = capacity;
            PricePerNight = pricePerNight;
            RoomGroupId = roomGroupId;
            Images = images;
        }

        public int MaxGuestsCount { get; }
        public double Capacity { get; }
        public decimal PricePerNight { get; }
        public string RoomGroupId { get; }
        public ICollection<MediaDto> Images { get; }

        private class Handler : AsyncRequestHandler<CreateRoomCommand>
        {
            private readonly IApplicationDbContext _applicationDb;
            private readonly ISecurityContext _securityContext;

            public Handler(IApplicationDbContext applicationDb, ISecurityContext securityContext)
            {
                _applicationDb = applicationDb;
                _securityContext = securityContext;
            }

            protected override async Task Handle(CreateRoomCommand request, CancellationToken cancellationToken)
            {
                var room = Room.New(
                    _securityContext.UserId,
                    request.RoomGroupId,
                    request.MaxGuestsCount,
                    request.Capacity,
                    request.PricePerNight);

                (room.LastModifiedBy, room.CreatedBy) = (_securityContext.UserId, _securityContext.UserId);

                _applicationDb.Room.Add(room);
                await _applicationDb.SaveChangesAsync(cancellationToken);
            }
        }
    }
}