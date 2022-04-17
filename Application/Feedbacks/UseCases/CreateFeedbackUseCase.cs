using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Feedbacks.Commands;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;

namespace HotelAutomationApp.Application.Feedbacks.UseCases;

public class CreateFeedbackUseCase : TransactionUseCase<CreateFeedbackRequest>
{
    private readonly IMediator _mediator;


    public CreateFeedbackUseCase(IApplicationDbContext applicationDb, IMediator mediator) : base(applicationDb)
    {
        _mediator = mediator;
    }

    protected override async Task HandleRequestAsync(CreateFeedbackRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CreateFeedbackCommand(
            request.AppealId,
            request.Title,
            request.Body,
            request.IsBodyHtml), CancellationToken.None);
    }
}

public class CreateFeedbackRequest : IRequest
{
    public string AppealId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public bool IsBodyHtml { get; set; }
}