using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.MediaFiles.Commands;
using MediatR;

namespace HotelAutomationApp.Application.MediaFiles.UseCases;

public class DeleteMediaUseCase : UseCase<DeleteMediaRequest>
{
    private readonly IMediator _mediator;

    public DeleteMediaUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task HandleRequestAsync(DeleteMediaRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteMediaCommand(request.MediaId), CancellationToken.None);
    }
}

public class DeleteMediaRequest : IRequest
{
    public string MediaId { get; set; }
}