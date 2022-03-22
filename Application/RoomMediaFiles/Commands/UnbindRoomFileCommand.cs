using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Z.EntityFramework.Plus;

namespace HotelAutomationApp.Application.RoomMediaFiles.Commands;

public class UnbindRoomFileCommand : IRequest
{
    public UnbindRoomFileCommand(string roomId, ICollection<string> fileIds)
    {
        RoomId = roomId;
        FileIds = fileIds;
    }

    public string RoomId { get; }
    public ICollection<string> FileIds { get;}

    private class Handler : AsyncRequestHandler<UnbindRoomFileCommand>
    {
        private readonly IApplicationDbContext _applicationDb;

        public Handler(IApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }

        protected override async Task Handle(UnbindRoomFileCommand request, CancellationToken cancellationToken) =>
            await _applicationDb.RoomFiles
                .Where(q => q.RoomId == request.RoomId && request.FileIds.Contains(q.FileMetadataId))
                .DeleteAsync(cancellationToken);
    }
}