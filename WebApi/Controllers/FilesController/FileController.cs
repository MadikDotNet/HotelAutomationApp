using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Application.Auth.Constants;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Application.MediaFiles.UseCases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers.FilesController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(PageResponse<FileMetadataDto>))]
        public async Task<IActionResult> View([FromQuery] ViewMediaRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> Download(
            [FromQuery] DownloadMediaRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            return File(result.Content, result.FileMetadata.FileType!);
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var result = await _mediator.Send(new UploadMediaRequest(file));

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteMediaRequest {FileId = id});

            return Ok(result);
        }
    }
}