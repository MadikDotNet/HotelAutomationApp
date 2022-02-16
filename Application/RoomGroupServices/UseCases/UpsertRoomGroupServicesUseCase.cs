using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.RoomGroupServices.Commands;
using HotelAutomationApp.Application.Services.Models;
using MediatR;

namespace HotelAutomationApp.Application.RoomGroupServices.UseCases;

public class UpsertRoomGroupServicesUseCase : UseCase<UpsertRoomGroupServicesRequest>
{
    private readonly IMediator _mediator;

    public UpsertRoomGroupServicesUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task HandleRequestAsync(
        UpsertRoomGroupServicesRequest request,
        CancellationToken cancellationToken) =>
        await _mediator.Send(new UpsertRoomGroupServiceCommand(
            request.RoomGroupId,
            request.Services), cancellationToken);
}

public class UpsertRoomGroupServicesRequest : IRequest
{
    public string RoomGroupId { get; set; }
    public ICollection<ServiceDto> Services { get; set; }
}