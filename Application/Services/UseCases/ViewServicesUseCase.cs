using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Common.Dictionary.Models.Requests;
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

public class ViewServicesRequest : ViewDictionaryListRequest, IRequest<PageResponse<ServiceDto>>
{
    public bool IsAdditional { get; set; }
}