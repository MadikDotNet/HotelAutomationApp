using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Application.Auth.Constants;
using HotelAutomationApp.Application.Rooms.Models;
using HotelAutomationApp.Application.Rooms.UseCases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers.Room
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ICollection<RoomDto>))]
        public async Task<IActionResult> View([FromQuery] ViewRoomsRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteRoom([FromBody] DeleteRoomRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateRoom([FromBody] UpdateRoomRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }
    }
}