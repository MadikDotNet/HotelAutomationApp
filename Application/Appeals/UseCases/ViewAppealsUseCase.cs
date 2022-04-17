using HotelAutomationApp.Application.Appeals.Models;
using HotelAutomationApp.Application.Appeals.Queries;
using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Domain.Models.Messaging.Appeals;
using HotelAutomationApp.Shared.Common.Abstractions;
using MediatR;

namespace HotelAutomationApp.Application.Appeals.UseCases;

public class ViewAppealsUseCase : UseCase<ViewAppealsRequest, PageResponse<AppealDto>>
{
    private readonly IMediator _mediator;

    public ViewAppealsUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<PageResponse<AppealDto>> HandleAsync(ViewAppealsRequest request,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new ViewAppealsQuery(
            request.PageRequest,
            request.Email,
            request.UserName,
            request.Status,
            request.FullMatching,
            request), cancellationToken);
    }
}

public class ViewAppealsRequest : UTCPeriod, IRequest<PageResponse<AppealDto>>
{
    public PageRequest? PageRequest { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public AppealStatus? Status { get; set; }
    public bool FullMatching { get; set; }
}