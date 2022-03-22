using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.RoomMediaFiles.Commands;
using HotelAutomationApp.Application.Rooms.Models;
using MediatR;

namespace HotelAutomationApp.Application.RoomMediaFiles.UseCases;

public class UpsertRoomFilesUseCase : UseCase<UpsertRoomFileRequest>
{
    private readonly IMediator _mediator;

    public UpsertRoomFilesUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task HandleRequestAsync(
        UpsertRoomFileRequest request,
        CancellationToken cancellationToken) => 
        await _mediator.Send(new UpsertRoomFilesCommand(request.RoomId, request.Files), CancellationToken.None);
}

public class UpsertRoomFileRequest : IRequest
{
    public string RoomId { get; set; }
    public ICollection<FileDto> Files { get; set; }
}