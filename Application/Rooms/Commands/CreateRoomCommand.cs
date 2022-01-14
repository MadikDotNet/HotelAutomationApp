using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HotelAutomationApp.Application.Rooms.Models;
using HotelAutomationApp.Domain.Models.Rooms;
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
            private readonly IMapper _mapper;
            private readonly ISecurityContext _securityContext;

            public Handler(IDbContext db, IMapper mapper, ISecurityContext securityContext)
            {
                _db = db;
                _mapper = mapper;
                _securityContext = securityContext;
            }

            protected override async Task Handle(CreateRoomCommand request, CancellationToken cancellationToken)
            {
                var room = Room.New(
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