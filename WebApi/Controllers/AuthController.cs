using System.Threading.Tasks;
using HotelAutomation.Application.Auth.Queries;
using HotelAutomation.Application.Auth.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomation.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var result = await _mediator.Send(request);
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            var result = await _mediator.Send(request); 
            
            return Ok(result);
        }
    }
}