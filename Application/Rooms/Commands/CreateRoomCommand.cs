using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Application.Rooms.Models;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using MediatR;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Rooms.Commands
{
    public class CreateRoomCommand : IRequest
    {
        public CreateRoomCommand(RoomDto roomDto)
        {
            RoomDto = roomDto;
        }

        public RoomDto RoomDto { get; set; }
        
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
                var room = Domain.Models.Rooms.Room.New(
                    _securityContext.UserId,
                    request.RoomDto.RoomGroupId,
                    request.RoomDto.MaxGuestsCount,
                    request.RoomDto.Capacity,
                    request.RoomDto.PricePerNight);
                
                (room.LastModifiedBy, room.CreatedBy) = (_securityContext.UserId, _securityContext.UserId);
                
                _db.Rooms.Add(room);
                await _db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}