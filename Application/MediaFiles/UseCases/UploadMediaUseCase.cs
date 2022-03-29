using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.MediaFiles.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelAutomationApp.Application.MediaFiles.UseCases;

public class UploadMediaUseCase : UseCase<UploadFileRequest, string>
{
    private readonly IMediator _mediator;

    public UploadMediaUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<string> HandleAsync(UploadFileRequest request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new UploadFileCommand(request.FormFile), CancellationToken.None);
    }
}

public class UploadFileRequest : IRequest<string>
{
    public UploadFileRequest()
    {
    }

    public UploadFileRequest(IFormFile formFile)
    {
        FormFile = formFile;
    }

    public IFormFile FormFile { get; set; }
}