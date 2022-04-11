using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Extensions;
using HotelAutomationApp.Application.Users.Models;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Users.Queries;

public class GetUserListQuery : IRequest<PageResponse<UserDto>>
{
    public GetUserListQuery(PageRequest? pageRequest, string? username, bool? fullMatching)
    {
        PageRequest = pageRequest;
        Username = username;
        FullMatching = fullMatching;
    }

    public PageRequest? PageRequest { get; }
    public string? Username { get; }
    public bool? FullMatching { get; }

    private class Handler : IRequestHandler<GetUserListQuery, PageResponse<UserDto>>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDb, IMapper mapper)
        {
            _applicationDb = applicationDb;
            _mapper = mapper;
        }

        public async Task<PageResponse<UserDto>> Handle(
            GetUserListQuery request,
            CancellationToken cancellationToken)
        {
            var users = _applicationDb.User.AsQueryable();

            if (request.Username is { })
            {
                users = users.Where(user =>
                    request.FullMatching.HasValue && (bool) request.FullMatching
                        ? user.UserName == request.Username
                        : user.UserName.Contains(request.Username));
            }

            return (await users
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken))
                .AsPageResponse(request.PageRequest);
        }
    }
}