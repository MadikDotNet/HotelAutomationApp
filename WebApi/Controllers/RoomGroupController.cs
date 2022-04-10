using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Application.ApplicationServices.Dictionary;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.RoomGroups.Models;
using HotelAutomationApp.Application.RoomGroups.UseCases;
using HotelAutomationApp.Domain.Models.RoomGroups;
using HotelAutomationApp.WebApi.Controllers.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers
{
    public class RoomGroupController : DictionaryController
        <RoomGroup, RoomGroupDto, DictionaryCrudService<RoomGroup, RoomGroupDto>>
    {
        private readonly IMediator _mediator;
        
        public RoomGroupController(DictionaryCrudService<RoomGroup, RoomGroupDto> dictionaryService, IMediator mediator) 
            : base(dictionaryService)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PageResponse<RoomGroupWithAvailableServicesDto>))]
        public async Task<IActionResult> ViewRoomGroupsWithAvailableServices(
            [FromQuery] ViewRoomGroupsWithAvailableServicesRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            return Ok(result);
        }
    }
}