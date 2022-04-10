using System;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;

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
            private readonly IApplicationDbContext _applicationDb;
            private readonly ISecurityContext _securityContext; 

            public Handler(IApplicationDbContext applicationDb, ISecurityContext securityContext)
            {
                _applicationDb = applicationDb;
                _securityContext = securityContext;
            }

            protected override async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
            {
                var room = await _applicationDb.Room.FindAsync(new object[]{request.RoomId}, cancellationToken);

                room!.DeletedBy = _securityContext.UserId;
                (room.IsDeleted, room.DeletedDate) = (true,  DateTime.UtcNow);
                
                await _applicationDb.SaveChangesAsync(cancellationToken);
            }
        }
    }
}