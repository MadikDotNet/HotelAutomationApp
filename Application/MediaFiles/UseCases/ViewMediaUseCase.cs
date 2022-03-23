using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Application.MediaFiles.Queries;
using MediatR;

namespace HotelAutomationApp.Application.MediaFiles.UseCases;

public class ViewMediaUseCase : UseCase<ViewMediaRequest, PageResponse<FileMetadataDto>>
{
    private readonly IMediator _mediator;

    public ViewMediaUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<PageResponse<FileMetadataDto>> HandleAsync(ViewMediaRequest request,
        CancellationToken cancellationToken) =>
        await _mediator.Send(new ViewMediaQuery(
            request.PageRequest, request.FullMatch, request.FileName, request.FileType), cancellationToken);
}

public class ViewMediaRequest : IRequest<PageResponse<FileMetadataDto>>
{
    public PageRequest? PageRequest { get; set; }
    public bool FullMatch { get; set; }
    public string? FileName { get; set; }
    public string? FileType { get; set; }
}