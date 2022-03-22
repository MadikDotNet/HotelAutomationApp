using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Extensions;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.MediaFiles.Queries;

public class ViewMediaQuery : IRequest<PageResponse<FileMetadataDto>>
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

    private class Handler : IRequestHandler<ViewMediaQuery, PageResponse<FileMetadataDto>>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDb, IMapper mapper)
        {
            _applicationDb = applicationDb;
            _mapper = mapper;
        }

        public async Task<PageResponse<FileMetadataDto>> Handle(ViewMediaQuery request, CancellationToken cancellationToken)
        {
            var response = _applicationDb.FileMetadata.AsQueryable();

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
                .ProjectTo<FileMetadataDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken))
                .AsPageResponse(request.PageRequest);
        }
    }
}