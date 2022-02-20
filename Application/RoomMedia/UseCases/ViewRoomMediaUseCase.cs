using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Application.RoomMedia.Queries;
using MediatR;

namespace HotelAutomationApp.Application.RoomMedia.UseCases;

public class ViewRoomMediaUseCase : UseCase<ViewRoomMediaRequest, PageResponse<MediaDto>>
{
    private readonly IMediator _mediator;

    public ViewRoomMediaUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<PageResponse<MediaDto>> HandleAsync(
        ViewRoomMediaRequest request,
        CancellationToken cancellationToken) =>
        await _mediator.Send(new ViewRoomMediaQuery(
                request.PageRequest,
                request.RoomId),
            cancellationToken);
}

public class ViewRoomMediaRequest : IRequest<PageResponse<MediaDto>>
{
    public PageRequest PageRequest { get; set; }
    public string? RoomId { get; set; }
}