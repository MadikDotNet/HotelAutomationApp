using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Application.MediaFiles.Queries;
using MediatR;

namespace HotelAutomationApp.Application.MediaFiles.UseCases;

public class DownloadMediaUseCase : UseCase<DownloadMediaRequest, FileBlob>
{
    private readonly IMediator _mediator;

    public DownloadMediaUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<FileBlob> HandleAsync(DownloadMediaRequest request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new DownloadMediaQuery(request.MediaId), cancellationToken);
    }
}

public class DownloadMediaRequest : IRequest<FileBlob>
{
    public DownloadMediaRequest()
    {
        
    }
    
    public DownloadMediaRequest(string mediaId)
    {
        MediaId = mediaId;
    }

    public string MediaId { get; set; }
}