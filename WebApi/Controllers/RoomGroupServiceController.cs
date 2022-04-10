using System.Net;
using System.Threading.Tasks;
using HotelAutomationApp.Application.Auth.Constants;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.RoomGroupServices.UseCases;
using HotelAutomationApp.Application.Services.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
public class RoomGroupServiceController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomGroupServiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PageResponse<ServiceDto>))]
    public async Task<IActionResult> View([FromQuery]ViewRoomGroupServicesRequest request)
    {
        var result = await _mediator.Send(request);
            
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Bind([FromBody]BindRoomGroupServicesRequest request)
    {
        var result = await _mediator.Send(request); 
            
        return Ok(result);
    }
        
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Unbind([FromBody]UnbindRoomGroupServiceRequest request)
    {
        var result = await _mediator.Send(request); 
            
        return Ok(result);
    }
}