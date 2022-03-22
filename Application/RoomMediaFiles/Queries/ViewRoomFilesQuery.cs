using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Extensions;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.RoomMediaFiles.Queries;

public class ViewRoomFilesQuery : IRequest<PageResponse<FileMetadataDto>>
{
    public ViewRoomFilesQuery(PageRequest? pageRequest, string? roomId)
    {
        PageRequest = pageRequest;
        RoomId = roomId;
    }

    public PageRequest? PageRequest { get; }
    public string? RoomId { get; }

    private class Handler : IRequestHandler<ViewRoomFilesQuery, PageResponse<FileMetadataDto>>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDb, IMapper mapper)
        {
            _applicationDb = applicationDb;
            _mapper = mapper;
        }

        public async Task<PageResponse<FileMetadataDto>> Handle(ViewRoomFilesQuery request,
            CancellationToken cancellationToken) =>
            (await _applicationDb.RoomFiles.AsQueryable()
                .Where(q => q.RoomId == request.RoomId)
                .Select(q => q.FileMetadata)
                .ProjectTo<FileMetadataDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken))
            .AsPageResponse(request.PageRequest);
    }
}