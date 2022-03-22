using HotelAutomationApp.Domain.Models.MediaFiles;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using HotelAutomationApp.Infrastructure.Interfaces.MediaFiles;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelAutomationApp.Application.MediaFiles.Commands;

public class UploadFileCommand : IRequest<string>
{
    public UploadFileCommand(IFormFile formFile)
    {
        FormFile = formFile;
    }

    public IFormFile FormFile { get; }

    private class Handler : IRequestHandler<UploadFileCommand, string>
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

        public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            await using var memoryStream = new MemoryStream();
            await request.FormFile.CopyToAsync(memoryStream, CancellationToken.None);

            var id = await _mediaStorage.UploadAsync(memoryStream.ToArray());

            var newFiles = new FileMetadata(id, request.FormFile.FileName, request.FormFile.ContentType)
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