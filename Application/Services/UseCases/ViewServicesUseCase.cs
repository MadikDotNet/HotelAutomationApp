using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Services.Models;
using HotelAutomationApp.Application.Services.Queries;
using MediatR;

namespace HotelAutomationApp.Application.Services.UseCases;

public class ViewServicesUseCase : UseCase<ViewServicesRequest, PageResponse<ServiceDto>>
{
    private readonly IMediator _mediator;

    public ViewServicesUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<PageResponse<ServiceDto>> HandleAsync(ViewServicesRequest request,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new ViewAdditionalServicesQuery(
            request.PageRequest,
            request.Code,
            request.Name,
            request.Description,
            request.FullMatching,
            request.IsAdditional), cancellationToken);
    }
}

public class ViewServicesRequest : IRequest<PageResponse<ServiceDto>>
{
    public PageRequest? PageRequest { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool FullMatching { get; set; }
    public bool IsAdditional { get; set; }
}
