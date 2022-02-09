using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.File.Models;
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
                request.RoomId,
                request.FullMatch,
                request.FileName,
                request.FileType),
            cancellationToken);
}

public class ViewRoomMediaRequest : IRequest<PageResponse<MediaDto>>
{
    public PageRequest PageRequest { get; }
    public string RoomId { get; }
    public bool FullMatch { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
}