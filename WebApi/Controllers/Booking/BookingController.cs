using System.Net;
using System.Threading.Tasks;
using HotelAutomationApp.Application.Bookings.Models;
using HotelAutomationApp.Application.Bookings.UseCases;
using HotelAutomationApp.Application.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers.Booking
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(PageResponse<BookingDto>))]
        public async Task<IActionResult> View([FromQuery] ViewBookingsRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}