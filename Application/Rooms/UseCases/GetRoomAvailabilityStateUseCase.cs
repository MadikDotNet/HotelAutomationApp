using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Rooms.Queries;
using MediatR;

namespace HotelAutomationApp.Application.Rooms.UseCases;

public class GetRoomAvailabilityStateUseCase : UseCase<GetRoomAvailabilityStateRequest, bool>
{
    private readonly IMediator _mediator;

    public GetRoomAvailabilityStateUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<bool> HandleAsync(
        GetRoomAvailabilityStateRequest request,
        CancellationToken cancellationToken) =>
        await _mediator.Send(new GetRoomAvailabilityStateQuery(request.RoomId, request), cancellationToken);
}

public class GetRoomAvailabilityStateRequest : UTCPeriod, IRequest<bool>
{
    public string RoomId { get; set; }
}