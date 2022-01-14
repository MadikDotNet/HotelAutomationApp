using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HotelAutomation.Domain.Models.Rooms;
using HotelAutomationApp.Application.Rooms.Models;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using MediatR;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Rooms.Commands
{
    public class UpdateRoomCommand : IRequest
    {
        public UpdateRoomCommand(RoomDto roomDto)
        {
            RoomDto = roomDto;
        }
        
        public RoomDto RoomDto { get; }
        
        private class Handler : AsyncRequestHandler<UpdateRoomCommand>
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

            protected override async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
            {
                var room = _mapper.Map<Room>(request.RoomDto);
                var images = _db.RoomImages.Where(q => q.RoomId == request.RoomDto.Id);
                _db.RoomImages.RemoveRange(images);
                await _db.SaveChangesAsync(cancellationToken);
                
                room.LastModifiedBy = _securityContext.UserId;
                
                _db.Rooms.Update(room);
                await _db.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}