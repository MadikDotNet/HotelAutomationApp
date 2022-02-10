using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.MediaFiles.Commands;
using HotelAutomationApp.Application.MediaFiles.Models;
using MediatR;

namespace HotelAutomationApp.Application.MediaFiles.UseCases;

public class UpsertMediaUseCase : UseCase<UpsertMediaRequest>
{
    private readonly IMediator _mediator;

    public UpsertMediaUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task HandleRequestAsync(UpsertMediaRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpsertMediaCommand(request.Media), CancellationToken.None);
    }
}

public class UpsertMediaRequest : IRequest
{
    public ICollection<MediaDto> Media { get; set; }
}