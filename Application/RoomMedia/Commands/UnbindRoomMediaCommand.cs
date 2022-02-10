using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Z.EntityFramework.Plus;

namespace HotelAutomationApp.Application.RoomMedia.Commands;

public class UnbindRoomMediaCommand : IRequest
{
    public UnbindRoomMediaCommand(string roomId, ICollection<string> mediaIds)
    {
        RoomId = roomId;
        MediaIds = mediaIds;
    }

    public string RoomId { get; }
    public ICollection<string> MediaIds { get;}

    private class Handler : AsyncRequestHandler<UnbindRoomMediaCommand>
    {
        private readonly IApplicationDbContext _applicationDb;

        public Handler(IApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }

        protected override async Task Handle(UnbindRoomMediaCommand request, CancellationToken cancellationToken) =>
            await _applicationDb.RoomMedia
                .Where(q => q.RoomId == request.RoomId && request.MediaIds.Contains(q.MediaId))
                .DeleteAsync(cancellationToken);
    }
}