using HotelAutomationApp.Application.Appeals.Commands;
using HotelAutomationApp.Application.Common;
using MediatR;

namespace HotelAutomationApp.Application.Appeals.UseCases;

public class CreateAnonymousAppealUseCase : UseCase<CreateAppealRequest>
{
    private readonly IMediator _mediator;

    public CreateAnonymousAppealUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task HandleRequestAsync(CreateAppealRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CreateAppealCommand(
                request.Email,
                request.Title,
                request.Body,
                request.UserName),
            cancellationToken);
    }
}

public class CreateAppealRequest : IRequest
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}