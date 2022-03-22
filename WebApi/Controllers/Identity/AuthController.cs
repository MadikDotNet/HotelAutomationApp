using System.Threading.Tasks;
using HotelAutomationApp.Application.Auth.UseCases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody]SignInRequest request)
        {
            var result = await _mediator.Send(request);
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody]SignUpRequest request)
        {
            var result = await _mediator.Send(request); 
            
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}