using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Application.RoomMedia.Commands;
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
            ICollection<MediaDto>? media)
        {
            MaxGuestsCount = maxGuestsCount;
            Capacity = capacity;
            PricePerNight = pricePerNight;
            RoomGroupId = roomGroupId;
            Media = media;
        }

        public int MaxGuestsCount { get; }
        public double Capacity { get; }
        public decimal PricePerNight { get; }
        public string RoomGroupId { get; }
        public ICollection<MediaDto>? Media { get; }

        private class Handler : AsyncRequestHandler<CreateRoomCommand>
        {
            private readonly IApplicationDbContext _applicationDb;
            private readonly ISecurityContext _securityContext;
            private readonly IMediator _mediator;

            public Handler(
                IApplicationDbContext applicationDb,
                ISecurityContext securityContext,
                IMediator mediator)
            {
                _applicationDb = applicationDb;
                _securityContext = securityContext;
                _mediator = mediator;
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
                await _applicationDb.SaveChangesAsync(CancellationToken.None);

                await _mediator.Send(new UpsertRoomMediaCommand(room.Id, request.Media), CancellationToken.None);
            }
        }
    }
}