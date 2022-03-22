using HotelAutomationApp.Application.MediaFiles.Commands;
using HotelAutomationApp.Application.Rooms.Models;
using HotelAutomationApp.Domain.Models.RoomMediaFiles;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Extensions;
using MediatR;

namespace HotelAutomationApp.Application.RoomMediaFiles.Commands;

public class UpsertRoomFilesCommand : IRequest
{
    public UpsertRoomFilesCommand(string roomId, ICollection<FileDto> files)
    {
        RoomId = roomId;
        Files = files;
    }

    public string RoomId { get; }
    public ICollection<FileDto> Files { get; }

    private class Handler : AsyncRequestHandler<UpsertRoomFilesCommand>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMediator _mediator;

        public Handler(IApplicationDbContext applicationDb, IMediator mediator)
        {
            _applicationDb = applicationDb;
            _mediator = mediator;
        }

        protected override async Task Handle(UpsertRoomFilesCommand request, CancellationToken cancellationToken)
        {
            var room = await _applicationDb.Room.FindAsync(request.RoomId);

            var newFiles = request.Files.ExcludeAfterFilter(q => q.Id is not { }, out var remain);

            var newFileIds = newFiles.Any()
                ? (await Task.WhenAll(newFiles.Select(async file =>
                    await _mediator.Send(new UploadFileCommand(file.File!), cancellationToken)))).ToList()
                : new List<string>();

            var newRoomFiles = (from fileId in newFileIds.Concat(remain.Select(q => q.Id))
                    join file in _applicationDb.FileMetadata on fileId equals file.Id
                    join roomFile in _applicationDb.RoomFiles on room.Id equals roomFile.RoomId into rmGp
                    where rmGp.All(q => q.FileMetadataId != file.Id)
                    select new RoomFile(Guid.NewGuid().ToString(), room.Id, file.Id))
                .ToList();

            _applicationDb.RoomFiles.AddRange(newRoomFiles);
            await _applicationDb.SaveChangesAsync(CancellationToken.None);
        }
    }
}