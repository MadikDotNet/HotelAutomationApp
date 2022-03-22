using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Common.Dictionary.Models.Requests;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.RoomGroups.Models;
using HotelAutomationApp.Application.RoomGroups.Queries;
using MediatR;

namespace HotelAutomationApp.Application.RoomGroups.UseCases;

public class ViewRoomGroupsWithAvailableServicesUseCase : UseCase
    <ViewRoomGroupsWithAvailableServicesRequest, PageResponse<RoomGroupWithAvailableServicesDto>>
{
    private readonly IMediator _mediator;

    public ViewRoomGroupsWithAvailableServicesUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<PageResponse<RoomGroupWithAvailableServicesDto>> HandleAsync(
        ViewRoomGroupsWithAvailableServicesRequest request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetRoomGroupsWithAvailableServicesQuery(
            request.PageRequest,
            request.Code,
            request.Name,
            request.Description,
            request.FullMatching), cancellationToken);
    }
}

public class ViewRoomGroupsWithAvailableServicesRequest : ViewDictionaryListRequest,
    IRequest<PageResponse<RoomGroupWithAvailableServicesDto>>
{
}