using HotelAutomationApp.Domain.Models.MediaFiles;
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

        public Handler(IApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }

        protected override async Task Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
        {
            var media = new Media {Id = request.MediaId};

            _applicationDb.Media.Attach(media);
            _applicationDb.Media.Remove(media);
            await _applicationDb.SaveChangesAsync(CancellationToken.None);
        }
    }
}