using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.RoomGroupServices.Queries;
using HotelAutomationApp.Application.Services.Models;
using MediatR;

namespace HotelAutomationApp.Application.RoomGroupServices.UseCases;

public class ViewRoomGroupServicesUseCase : UseCase<ViewRoomGroupServicesRequest, ICollection<ServiceDto>>
{
    private readonly IMediator _mediator;

    public ViewRoomGroupServicesUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<ICollection<ServiceDto>> HandleAsync(
        ViewRoomGroupServicesRequest request,
        CancellationToken cancellationToken) =>
        await _mediator.Send(new ViewRoomGroupServicesQuery(
            request.PageRequest,
            request.RoomGroupId,
            request.Code,
            request.Name,
            request.Description,
            request.FullMatching), cancellationToken);
}

public class ViewRoomGroupServicesRequest : IRequest<ICollection<ServiceDto>>
{
    public PageRequest PageRequest { get; }
    public string RoomGroupId { get; }
    public string? Code { get; }
    public string? Name { get; }
    public string? Description { get; }
    public bool FullMatching { get; }
}