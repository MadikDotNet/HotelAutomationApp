using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers.RoomGroup
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoomGroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}