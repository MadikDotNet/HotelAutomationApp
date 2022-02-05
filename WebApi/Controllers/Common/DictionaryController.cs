using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Application.ApplicationServices.Dictionary;
using HotelAutomationApp.Application.Auth.Constants;
using HotelAutomationApp.Application.Common.Dictionary.Models;
using HotelAutomationApp.Application.Common.Dictionary.Models.Requests;
using HotelAutomationApp.Application.Common.Dictionary.Models.Responses;
using HotelAutomationApp.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers.Common;

[ApiController]
[Route("api/dictionary/[controller]/[action]")]
public class DictionaryController
    <TDictionary, TDictionaryDto, TDictionaryService> : ControllerBase
    where TDictionary : BaseDictionary<TDictionary>, new()
    where TDictionaryDto : BaseDictionaryDto
    where TDictionaryService : DictionaryCrudService<TDictionary, TDictionaryDto>
{
    private readonly TDictionaryService _dictionaryService;

    public DictionaryController(TDictionaryService dictionaryService)
    {
        _dictionaryService = dictionaryService;
    }

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ViewDictionaryListResponse<>))]
    public async Task<IActionResult> ViewAsList(
        [FromQuery] ViewDictionaryListRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _dictionaryService.ViewAsList(request.PageRequest, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = nameof(AuthorizationPolicies.RequireAdminRole))]
    public async Task<IActionResult> Create(
        [FromBody] CreateDictionaryItemRequest<TDictionary, TDictionaryDto> request)
    {
        await _dictionaryService.Create(request.DictionaryDto);

        return Ok();
    }

    [HttpPut]
    [Authorize(Policy = nameof(AuthorizationPolicies.RequireAdminRole))]
    public async Task<IActionResult> Update([FromBody] UpdateDictionaryItemRequest<TDictionaryDto> request)
    {
        await _dictionaryService.Update(request.DictionaryDto);

        return Ok();
    }

    [HttpDelete]
    [Authorize(Policy = nameof(AuthorizationPolicies.RequireAdminRole))]
    public async Task<IActionResult> Delete([FromBody] DeleteDictionaryItemRequest request)
    {
        await _dictionaryService.Delete(request.Id);

        return Ok();
    }
}