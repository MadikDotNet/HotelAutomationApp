using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.RoomMediaFiles.Commands;
using MediatR;

namespace HotelAutomationApp.Application.RoomMediaFiles.UseCases;

public class UnbindRoomMediaUseCase : UseCase<UnbindRoomMediaRequest>
{
    private readonly IMediator _mediator;

    public UnbindRoomMediaUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task HandleRequestAsync(
        UnbindRoomMediaRequest request,
        CancellationToken cancellationToken) =>
        await _mediator.Send(new UnbindRoomMediaCommand(request.RoomId, request.MediaIds), CancellationToken.None);
}

public class UnbindRoomMediaRequest : IRequest
{
    public string RoomId { get; set; }
    public ICollection<string> MediaIds { get; set; }
}