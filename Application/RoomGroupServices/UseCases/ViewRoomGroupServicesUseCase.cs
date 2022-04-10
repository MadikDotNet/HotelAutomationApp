using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.RoomGroupServices.Queries;
using HotelAutomationApp.Application.Services.Models;
using MediatR;

namespace HotelAutomationApp.Application.RoomGroupServices.UseCases;

public class ViewRoomGroupServicesUseCase : UseCase<ViewRoomGroupServicesRequest, PageResponse<ServiceDto>>
{
    private readonly IMediator _mediator;

    public ViewRoomGroupServicesUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<PageResponse<ServiceDto>> HandleAsync(
        ViewRoomGroupServicesRequest request,
        CancellationToken cancellationToken) =>
        await _mediator.Send(new ViewRoomGroupServicesQuery(
            request.PageRequest,
            request.RoomGroupId), cancellationToken);
}

public class ViewRoomGroupServicesRequest : IRequest<PageResponse<ServiceDto>>
{
    public PageRequest? PageRequest { get; set; }
    public string RoomGroupId { get; set; }
}