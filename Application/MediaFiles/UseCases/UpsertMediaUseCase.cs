using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.File.Models;
using HotelAutomationApp.Application.MediaFiles.Commands;
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