using HotelAutomationApp.Application.Bookings.Commands;
using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;

namespace HotelAutomationApp.Application.Bookings.UseCases;

public class CreateBookingUseCase : TransactionUseCase<CreateBookingRequest>
{
    private readonly IMediator _mediator;
    private readonly ISecurityContext _securityContext;

    public CreateBookingUseCase(
        IApplicationDbContext applicationDb,
        IMediator mediator,
        ISecurityContext securityContext) : base(applicationDb)
    {
        _mediator = mediator;
        _securityContext = securityContext;
    }

    protected override async Task HandleRequestAsync(CreateBookingRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CreateBookingCommand(
            _securityContext.UserId,
            request.Roomid,
            request.ServiceIds,
            request.DateFrom,
            request.DateTo), CancellationToken.None);
    }
}

public class CreateBookingRequest : Period, IRequest
{
    public string Roomid { get; set; }
    public string[] ServiceIds { get; set; }
}