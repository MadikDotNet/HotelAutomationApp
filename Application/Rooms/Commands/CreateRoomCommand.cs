using HotelAutomationApp.Application.File.Models;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using MediatR;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Rooms.Commands
{
    public class CreateRoomCommand : IRequest
    {
        public CreateRoomCommand(
            int maxGuestsCount,
            double capacity,
            decimal pricePerNight,
            string roomGroupId,
            ICollection<ImageDto> images)
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
        public ICollection<ImageDto> Images { get; }

        private class Handler : AsyncRequestHandler<CreateRoomCommand>
        {
            private readonly IDbContext _db;
            private readonly ISecurityContext _securityContext;

            public Handler(IDbContext db, ISecurityContext securityContext)
            {
                _db = db;
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

                _db.Rooms.Add(room);
                await _db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}