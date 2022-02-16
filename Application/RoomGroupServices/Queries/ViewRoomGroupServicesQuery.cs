using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Extensions.IQueryable;
using HotelAutomationApp.Application.Services.Models;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.RoomGroupServices.Queries;

public class ViewRoomGroupServicesQuery : IRequest<ICollection<ServiceDto>>
{
    public ViewRoomGroupServicesQuery(
        PageRequest pageRequest,
        string roomGroupId,
        string? code,
        string? name,
        string? description,
        bool fullMatching)
    {
        PageRequest = pageRequest;
        RoomGroupId = roomGroupId;
        Code = code;
        Name = name;
        Description = description;
        FullMatching = fullMatching;
    }

    public PageRequest PageRequest { get; }
    public string RoomGroupId { get; }
    public string? Code { get; }
    public string? Name { get; }
    public string? Description { get; }
    public bool FullMatching { get; }

    private class Handler : IRequestHandler<ViewRoomGroupServicesQuery, ICollection<ServiceDto>>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDb, IMapper mapper)
        {
            _applicationDb = applicationDb;
            _mapper = mapper;
        }

        public async Task<ICollection<ServiceDto>> Handle(
            ViewRoomGroupServicesQuery request,
            CancellationToken cancellationToken) => 
        (await _applicationDb.RoomGroupService
            .Where(q => q.RoomGroupId == request.RoomGroupId)
            .Select(q => q.Service)
            .Filter(request.Code, request.Name, request.Description, request.FullMatching)
            .ProjectTo<ServiceDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken))
        .AsRecursiveTree(parent => parent.Id, child => child.ParentId).ToList();
    }
}