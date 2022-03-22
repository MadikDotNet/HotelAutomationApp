using AutoMapper;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Infrastructure.Interfaces.MediaFiles;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Common;
using MediatR;

namespace HotelAutomationApp.Application.MediaFiles.Queries;

public class DownloadMediaQuery : IRequest<FileBlob>
{
    public DownloadMediaQuery(string id)
    {
        Id = id;
    }
    
    public string Id { get; }
    
    private class Handler : IRequestHandler<DownloadMediaQuery, FileBlob>
    {
        private readonly IMediaStorage _mediaStorage;
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IMediaStorage mediaStorage, IApplicationDbContext applicationDb, IMapper mapper)
        {
            _mediaStorage = mediaStorage;
            _applicationDb = applicationDb;
            _mapper = mapper;
        }

        public async Task<FileBlob> Handle(DownloadMediaQuery request, CancellationToken cancellationToken)
        {
            var fileMetadata = await _applicationDb.FileMetadata.FindAsync(request.Id.YieldObjectArray(), cancellationToken);

            var fileContent = await _mediaStorage.DownloadAsync(request.Id);

            return new FileBlob
            {
                Content = fileContent,
                FileMetadata = _mapper.Map<FileMetadataDto>(fileMetadata)
            };
        }
    }
}