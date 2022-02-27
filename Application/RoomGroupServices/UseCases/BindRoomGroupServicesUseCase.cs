using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.RoomGroupServices.Commands;
using HotelAutomationApp.Application.Services.Models;
using MediatR;

namespace HotelAutomationApp.Application.RoomGroupServices.UseCases;

public class BindRoomGroupServicesUseCase : UseCase<BindRoomGroupServicesRequest>
{
    private readonly IMediator _mediator;

    public BindRoomGroupServicesUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task HandleRequestAsync(
        BindRoomGroupServicesRequest request,
        CancellationToken cancellationToken) =>
        await _mediator.Send(new BindRoomGroupServiceCommand(
            request.RoomGroupId,
            request.ServiceIds), cancellationToken);
}

public class BindRoomGroupServicesRequest : IRequest
{
    public string RoomGroupId { get; set; }
    public ICollection<string> ServiceIds { get; set; }
}