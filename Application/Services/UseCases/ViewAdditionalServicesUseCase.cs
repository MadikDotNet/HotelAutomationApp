using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Services.Models;
using HotelAutomationApp.Application.Services.Queries;
using MediatR;

namespace HotelAutomationApp.Application.Services.UseCases;

public class ViewAdditionalServicesUseCase : UseCase<ViewAdditionalServicesRequest, PageResponse<ServiceDto>>
{
    private readonly IMediator _mediator;

    public ViewAdditionalServicesUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<PageResponse<ServiceDto>> HandleAsync(ViewAdditionalServicesRequest request,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new ViewAdditionalServicesQuery(
            request.PageRequest,
            request.Code,
            request.Name,
            request.Description,
            request.FullMatching), cancellationToken);
    }
}

public class ViewAdditionalServicesRequest : IRequest<PageResponse<ServiceDto>>
{
    public PageRequest? PageRequest { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool FullMatching { get; set; }
}
