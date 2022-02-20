using System.Net;
using System.Threading.Tasks;
using HotelAutomationApp.Application.Auth.Constants;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Application.RoomMedia.UseCases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers.MediaFiles
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    public class RoomMediaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomMediaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PageResponse<MediaDto>))]
        public async Task<IActionResult> View([FromQuery]ViewRoomMediaRequest request)
        {
            var result = await _mediator.Send(request);
            
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Upsert([FromBody]UpsertRoomMediaRequest request)
        {
            var result = await _mediator.Send(request); 
            
            return Ok(result);
        }
        
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Unbind([FromBody]UnbindRoomMediaRequest request)
        {
            var result = await _mediator.Send(request); 
            
            return Ok(result);
        }
    }
}