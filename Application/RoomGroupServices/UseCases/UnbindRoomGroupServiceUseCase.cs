using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.RoomGroupServices.Commands;
using MediatR;

namespace HotelAutomationApp.Application.RoomGroupServices.UseCases;

public class UnbindRoomGroupServiceUseCase : UseCase<UnbindRoomGroupServiceRequest>
{
    private readonly IMediator _mediator;

    public UnbindRoomGroupServiceUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task HandleRequestAsync(
        UnbindRoomGroupServiceRequest request,
        CancellationToken cancellationToken) =>
        await _mediator.Send(new UnbindRoomGroupServiceCommand(
            request.RoomGroupId,
            request.ServiceIds), cancellationToken);
}

public class UnbindRoomGroupServiceRequest : IRequest
{
    public string RoomGroupId { get; set; }
    public ICollection<string> ServiceIds { get; set; }
}