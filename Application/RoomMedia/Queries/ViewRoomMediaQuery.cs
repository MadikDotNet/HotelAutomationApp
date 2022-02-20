using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Extensions;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.RoomMedia.Queries;

public class ViewRoomMediaQuery : IRequest<PageResponse<MediaDto>>
{
    public ViewRoomMediaQuery(PageRequest pageRequest, string? roomId)
    {
        PageRequest = pageRequest;
        RoomId = roomId;
    }

    public PageRequest PageRequest { get; }
    public string? RoomId { get; }

    private class Handler : IRequestHandler<ViewRoomMediaQuery, PageResponse<MediaDto>>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDb, IMapper mapper)
        {
            _applicationDb = applicationDb;
            _mapper = mapper;
        }

        public async Task<PageResponse<MediaDto>> Handle(ViewRoomMediaQuery request,
            CancellationToken cancellationToken) =>
            (await _applicationDb.RoomMedia.AsQueryable()
                .Where(q => q.RoomId == request.RoomId)
                .Select(q => q.Media)
                .ProjectTo<MediaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken))
            .AsPageResponse(request.PageRequest);
    }
}