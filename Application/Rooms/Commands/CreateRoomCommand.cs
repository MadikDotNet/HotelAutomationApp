using HotelAutomationApp.Application.Extensions;
using HotelAutomationApp.Application.RoomMediaFiles.Commands;
using HotelAutomationApp.Application.Rooms.Models;
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
            ICollection<FileDto>? files)
        {
            MaxGuestsCount = maxGuestsCount;
            Capacity = capacity;
            PricePerNight = pricePerNight;
            RoomGroupId = roomGroupId;
            Files = files;
        }

        public int MaxGuestsCount { get; }
        public double Capacity { get; }
        public decimal PricePerNight { get; }
        public string RoomGroupId { get; }
        public ICollection<FileDto>? Files { get; }

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

                room.CreatedBy(_securityContext.UserId);

                _applicationDb.Room.Add(room);
                await _applicationDb.SaveChangesAsync(CancellationToken.None);

                if (request.Files is { })
                {
                    await _mediator.Send(new UpsertRoomFilesCommand(room.Id, request.Files), CancellationToken.None);
                }
            }
        }
    }
}