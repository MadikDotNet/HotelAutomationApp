using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Application.ApplicationServices.Dictionary;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Services.Models;
using HotelAutomationApp.Application.Services.UseCases;
using HotelAutomationApp.Domain.Models.Services;
using HotelAutomationApp.WebApi.Controllers.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers.Services;

public class ServiceController : TreeDictionaryController
    <Service, ServiceDto, TreeDictionaryCrudService<Service, ServiceDto>>
{
    private readonly IMediator _mediator;

    public ServiceController(TreeDictionaryCrudService<Service, ServiceDto> dictionaryService, IMediator mediator)
        : base(dictionaryService)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(PageResponse<ServiceDto>))]
    public async Task<IActionResult> ViewWithFilter(
        [FromQuery]ViewServicesRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}