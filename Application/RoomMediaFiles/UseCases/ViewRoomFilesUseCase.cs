using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Application.RoomMediaFiles.Queries;
using MediatR;

namespace HotelAutomationApp.Application.RoomMediaFiles.UseCases;

public class ViewRoomFilesUseCase : UseCase<ViewRoomMediaRequest, PageResponse<FileMetadataDto>>
{
    private readonly IMediator _mediator;

    public ViewRoomFilesUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<PageResponse<FileMetadataDto>> HandleAsync(
        ViewRoomMediaRequest request,
        CancellationToken cancellationToken) =>
        await _mediator.Send(new ViewRoomFilesQuery(
                request.PageRequest,
                request.RoomId),
            cancellationToken);
}

public class ViewRoomMediaRequest : IRequest<PageResponse<FileMetadataDto>>
{
    public PageRequest? PageRequest { get; set; }
    public string? RoomId { get; set; }
}