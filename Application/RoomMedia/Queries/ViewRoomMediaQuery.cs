using AutoMapper;
using HotelAutomationApp.Application.Common.Extensions;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.File.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.RoomMedia.Queries;

public class ViewRoomMediaQuery : IRequest<PageResponse<MediaDto>>
{
    public ViewRoomMediaQuery(
        PageRequest pageRequest,
        string roomId,
        bool fullMatch,
        string fileName,
        string fileType)
    {
        PageRequest = pageRequest;
        RoomId = roomId;
        FullMatch = fullMatch;
        FileName = fileName;
        FileType = fileType;
    }
    
    public PageRequest PageRequest { get; }
    public string RoomId { get; }
    public bool FullMatch { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }

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
            CancellationToken cancellationToken)
        {
            var response = _applicationDb.RoomMedia.AsQueryable()
                .Where(q => q.RoomId == request.RoomId);

            if (!string.IsNullOrEmpty(request.FileName))
            {
                response = request.FullMatch
                    ? response.Where(q => q.Media.FileName == request.FileName)
                    : response.Where(q => q.Media.FileName.Contains(request.FileName));
            }

            if (!string.IsNullOrEmpty(request.FileType))
            {
                response = request.FullMatch
                    ? response.Where(q => q.Media.FileType == request.FileName)
                    : response.Where(q => q.Media.FileType.Contains(request.FileName));
            }

            return (await response.Include(q => q.Media)
                    .ToListAsync(cancellationToken))
                .Select(q => _mapper.Map<MediaDto>(q.Media))
                .AsPageResponse(request.PageRequest);
        }
    }
}