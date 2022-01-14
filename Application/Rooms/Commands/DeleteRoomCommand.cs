using System;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using MediatR;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Rooms.Commands
{
    public class DeleteRoomCommand : IRequest
    {
        public DeleteRoomCommand(string roomId)
        {
            RoomId = roomId;
        }

        public string RoomId { get; }
        
        private class Handler : AsyncRequestHandler<DeleteRoomCommand>
        {
            private readonly IDbContext _db;
            private readonly ISecurityContext _securityContext; 

            public Handler(IDbContext db, ISecurityContext securityContext)
            {
                _db = db;
                _securityContext = securityContext;
            }

            protected override async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
            {
                var room = await _db.Rooms.FindAsync(request.RoomId, cancellationToken);

                room!.DeletedBy = _securityContext.UserId;
                (room.IsDeleted, room.DeletedDate) = (true, DateTime.Now);
                
                await _db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}