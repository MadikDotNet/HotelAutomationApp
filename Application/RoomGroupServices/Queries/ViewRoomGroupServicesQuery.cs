using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Extensions;
using HotelAutomationApp.Application.Extensions.IQueryable;
using HotelAutomationApp.Application.Services.Models;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.RoomGroupServices.Queries;

public class ViewRoomGroupServicesQuery : IRequest<PageResponse<ServiceDto>>
{
    public ViewRoomGroupServicesQuery(
        PageRequest? pageRequest,
        string roomGroupId)
    {
        PageRequest = pageRequest;
        RoomGroupId = roomGroupId;
    }

    public PageRequest? PageRequest { get; }
    public string RoomGroupId { get; }

    private class Handler : IRequestHandler<ViewRoomGroupServicesQuery, PageResponse<ServiceDto>>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDb, IMapper mapper)
        {
            _applicationDb = applicationDb;
            _mapper = mapper;
        }

        public async Task<PageResponse<ServiceDto>> Handle(
            ViewRoomGroupServicesQuery request,
            CancellationToken cancellationToken) =>
            (await _applicationDb.RoomGroupService
                .Where(q => q.RoomGroupId == request.RoomGroupId)
                .Select(q => q.Service)
                .ProjectTo<ServiceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken))
            .AsPageResponse(request.PageRequest);
    }
}