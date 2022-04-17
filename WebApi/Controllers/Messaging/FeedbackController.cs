using System.Threading.Tasks;
using HotelAutomationApp.Application.Auth.Constants;
using HotelAutomationApp.Application.Feedbacks.UseCases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers.Messaging;

[ApiController]
[Route("api/[controller]/[action]")]
public class FeedbackController
{
    private readonly IMediator _mediator;

    public FeedbackController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackRequest request)
    {
        await _mediator.Send(request);

        return new OkResult();
    }
}