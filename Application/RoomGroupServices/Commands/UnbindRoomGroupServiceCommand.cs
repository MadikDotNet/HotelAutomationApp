using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Z.EntityFramework.Plus;

namespace HotelAutomationApp.Application.RoomGroupServices.Commands;

public class UnbindRoomGroupServiceCommand : IRequest
{
    public UnbindRoomGroupServiceCommand(string roomGroupId, ICollection<string> serviceIds)
    {
        RoomGroupId = roomGroupId;
        ServiceIds = serviceIds;
    }

    public string RoomGroupId { get; }
    public ICollection<string> ServiceIds { get;}

    private class Handler : AsyncRequestHandler<UnbindRoomGroupServiceCommand>
    {
        private readonly IApplicationDbContext _applicationDb;

        public Handler(IApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }

        protected override async Task Handle(
            UnbindRoomGroupServiceCommand request,
            CancellationToken cancellationToken) =>
            await _applicationDb.RoomGroupService
                .Where(q => q.RoomGroupId == request.RoomGroupId && request.ServiceIds.Contains(q.ServiceId))
                .DeleteAsync(cancellationToken);
    }
}