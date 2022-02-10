using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Application.RoomMedia.Commands;
using MediatR;

namespace HotelAutomationApp.Application.RoomMedia.UseCases;

public class UpsertRoomMediaUseCase : UseCase<UpsertRoomMediaRequest>
{
    private readonly IMediator _mediator;

    public UpsertRoomMediaUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task HandleRequestAsync(
        UpsertRoomMediaRequest request,
        CancellationToken cancellationToken) => 
        await _mediator.Send(new UpsertRoomMediaCommand(request.RoomId, request.Media), CancellationToken.None);
    
}

public class UpsertRoomMediaRequest : IRequest
{
    public string RoomId { get; set; }
    public ICollection<MediaDto> Media { get; set; }
}