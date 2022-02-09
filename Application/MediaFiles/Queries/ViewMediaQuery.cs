using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common.Extensions;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.File.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.MediaFiles.Queries;

public class ViewMediaQuery : IRequest<PageResponse<MediaDto>>
{
    public ViewMediaQuery(PageRequest pageRequest, bool fullMatch, string fileName, string fileType)
    {
        PageRequest = pageRequest;
        FullMatch = fullMatch;
        FileName = fileName;
        FileType = fileType;
    }

    public PageRequest PageRequest { get; }
    public bool FullMatch { get; }
    public string FileName { get; }
    public string FileType { get; }

    private class Handler : IRequestHandler<ViewMediaQuery, PageResponse<MediaDto>>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDb, IMapper mapper)
        {
            _applicationDb = applicationDb;
            _mapper = mapper;
        }

        public async Task<PageResponse<MediaDto>> Handle(ViewMediaQuery request, CancellationToken cancellationToken)
        {
            var response = _applicationDb.Media.AsQueryable();

            if (!string.IsNullOrEmpty(request.FileName))
            {
                response = request.FullMatch
                    ? response.Where(q => q.FileName == request.FileName)
                    : response.Where(q => q.FileName.Contains(request.FileName));
            }

            if (!string.IsNullOrEmpty(request.FileType))
            {
                response = request.FullMatch
                    ? response.Where(q => q.FileType == request.FileName)
                    : response.Where(q => q.FileType.Contains(request.FileName));
            }

            return (await response
                .ProjectTo<MediaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken))
                .AsPageResponse(request.PageRequest);
        }
    }
}