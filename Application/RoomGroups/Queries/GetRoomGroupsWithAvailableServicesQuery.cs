using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Extensions;
using HotelAutomationApp.Application.Extensions.IQueryable;
using HotelAutomationApp.Application.RoomGroups.Models;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.RoomGroups.Queries;

public class GetRoomGroupsWithAvailableServicesQuery : IRequest<PageResponse<RoomGroupWithAvailableServicesDto>>
{
    public GetRoomGroupsWithAvailableServicesQuery(
        PageRequest pageRequest,
        string? code,
        string? name,
        string? description,
        bool fullMatching)
    {
        PageRequest = pageRequest;
        Code = code;
        Name = name;
        Description = description;
        FullMatching = fullMatching;
    }

    public PageRequest PageRequest { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool FullMatching { get; set; }

    private class Handler : IRequestHandler
        <GetRoomGroupsWithAvailableServicesQuery, PageResponse<RoomGroupWithAvailableServicesDto>>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDb, IMapper mapper)
        {
            _applicationDb = applicationDb;
            _mapper = mapper;
        }

        public async Task<PageResponse<RoomGroupWithAvailableServicesDto>> Handle(
            GetRoomGroupsWithAvailableServicesQuery request,
            CancellationToken cancellationToken) =>
            (await _applicationDb.RoomGroup
                .Filter(request.Code, request.Name, request.Description, request.FullMatching)
                .ProjectTo<RoomGroupWithAvailableServicesDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken))
            .AsPageResponse(request.PageRequest);
    }
}