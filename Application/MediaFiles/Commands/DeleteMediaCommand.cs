using HotelAutomationApp.Domain.Models.MediaFiles;
using HotelAutomationApp.Infrastructure.Interfaces.MediaFiles;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;

namespace HotelAutomationApp.Application.MediaFiles.Commands;

public class DeleteMediaCommand : IRequest
{
    public DeleteMediaCommand(string mediaId)
    {
        MediaId = mediaId;
    }

    public string MediaId { get; }

    private class Handler : AsyncRequestHandler<DeleteMediaCommand>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMediaStorage _mediaStorage;

        public Handler(IApplicationDbContext applicationDb, IMediaStorage mediaStorage)
        {
            _applicationDb = applicationDb;
            _mediaStorage = mediaStorage;
        }

        protected override async Task Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
        {
            var fileMetadata = new FileMetadata {Id = request.MediaId};

            _applicationDb.FileMetadata.Attach(fileMetadata);
            _applicationDb.FileMetadata.Remove(fileMetadata);
            await _applicationDb.SaveChangesAsync(CancellationToken.None);

            await _mediaStorage.RemoveAsync(fileMetadata.Id);
        }
    }
}