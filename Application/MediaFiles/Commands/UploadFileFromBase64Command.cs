using HotelAutomationApp.Application.Rooms.Models;
using HotelAutomationApp.Domain.Models.MediaFiles;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using HotelAutomationApp.Infrastructure.Interfaces.MediaFiles;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;

namespace HotelAutomationApp.Application.MediaFiles.Commands;

public class UploadFileFromBase64Command : IRequest<string>
{
    public UploadFileFromBase64Command(FileDto file)
    {
        File = file;
    }

    public FileDto File { get; }

    private class Handler : IRequestHandler<UploadFileFromBase64Command, string>
    {
        private readonly IMediaStorage _mediaStorage;
        private readonly IApplicationDbContext _applicationDb;
        private readonly ISecurityContext _securityContext;

        public Handler(
            IMediaStorage mediaStorage,
            IApplicationDbContext applicationDb,
            ISecurityContext securityContext)
        {
            _mediaStorage = mediaStorage;
            _applicationDb = applicationDb;
            _securityContext = securityContext;
        }

        public async Task<string> Handle(UploadFileFromBase64Command request, CancellationToken cancellationToken)
        {
            await using var memoryStream = new MemoryStream();

            await memoryStream.WriteAsync(Convert.FromBase64String(request.File.Content!), CancellationToken.None);

            var id = await _mediaStorage.UploadAsync(memoryStream.ToArray());

            var newFiles = new FileMetadata(id, request.File.FileName!, request.File.FileType!)
            {
                CreatedBy = _securityContext.UserId,
                LastModifiedBy = _securityContext.UserId
            };

            _applicationDb.FileMetadata.Add(newFiles);

            await _applicationDb.SaveChangesAsync(CancellationToken.None);

            return id;
        }
    }
}