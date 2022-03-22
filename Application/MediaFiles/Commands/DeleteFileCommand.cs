using HotelAutomationApp.Domain.Models.MediaFiles;
using HotelAutomationApp.Infrastructure.Interfaces.MediaFiles;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;

namespace HotelAutomationApp.Application.MediaFiles.Commands;

public class DeleteFileCommand : IRequest
{
    public DeleteFileCommand(string fileId)
    {
        FileId = fileId;
    }

    public string FileId { get; }

    private class Handler : AsyncRequestHandler<DeleteFileCommand>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMediaStorage _mediaStorage;

        public Handler(IApplicationDbContext applicationDb, IMediaStorage mediaStorage)
        {
            _applicationDb = applicationDb;
            _mediaStorage = mediaStorage;
        }

        protected override async Task Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var fileMetadata = new FileMetadata {Id = request.FileId};

            _applicationDb.FileMetadata.Attach(fileMetadata);
            _applicationDb.FileMetadata.Remove(fileMetadata);
            await _applicationDb.SaveChangesAsync(CancellationToken.None);

            await _mediaStorage.RemoveAsync(fileMetadata.Id);
        }
    }
}