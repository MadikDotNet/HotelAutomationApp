using HotelAutomationApp.Application.Bookings.Commands;
using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;

namespace HotelAutomationApp.Application.Bookings.UseCases;

public class CreateBookingUseCase : TransactionUseCase<CreateBookingRequest>
{
    private readonly IMediator _mediator;

    public CreateBookingUseCase(IApplicationDbContext applicationDb, IMediator mediator) : base(applicationDb)
    {
        _mediator = mediator;
    }

    protected override async Task HandleRequestAsync(CreateBookingRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CreateBookingCommand(
            request.ClientId,
            request.Roomid,
            request.ServiceIds,
            request.DateFrom,
            request.DateTo), CancellationToken.None);
    }
}

public class CreateBookingRequest : Period, IRequest
{
    public string ClientId { get; set; }
    public string Roomid { get; set; }
    public string[] ServiceIds { get; set; }
}