using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Application.Appeals.UseCases;
using HotelAutomationApp.Application.Auth.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers.Messaging;

[ApiController]
[Route("api/[controller]/[action]")]
public class AppealController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppealController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    public async Task<IActionResult> ViewAppeals(
        [FromQuery] ViewAppealsRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAnonymousAppeal(
        [FromBody] CreateAppealRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}