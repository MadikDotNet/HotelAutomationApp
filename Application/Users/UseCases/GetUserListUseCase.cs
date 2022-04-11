using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Users.Models;
using HotelAutomationApp.Application.Users.Queries;
using MediatR;

namespace HotelAutomationApp.Application.Users.UseCases;

public class GetUserListUseCase : UseCase<GetUserListRequest, PageResponse<UserDto>>
{
    private readonly IMediator _mediator;

    public GetUserListUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<PageResponse<UserDto>> HandleAsync(GetUserListRequest request,
        CancellationToken cancellationToken) =>
        await _mediator.Send(new GetUserListQuery(
            request.PageRequest,
            request.Username,
            request.FullMatching), cancellationToken);
}

public class GetUserListRequest : IRequest<PageResponse<UserDto>>
{
    public PageRequest? PageRequest { get; set; }
    public string? Username { get; set; }
    public bool? FullMatching { get; set; }
}