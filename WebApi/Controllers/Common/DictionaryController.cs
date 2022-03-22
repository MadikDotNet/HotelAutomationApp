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
public abstract class DictionaryController
    <TDictionary, TDictionaryDto, TDictionaryService> : ControllerBase
    where TDictionary : BaseDictionary, new()
    where TDictionaryDto : BaseDictionaryDto
    where TDictionaryService : DictionaryCrudService<TDictionary, TDictionaryDto>
{
    protected readonly TDictionaryService DictionaryService;

    protected DictionaryController(TDictionaryService dictionaryService)
    {
        DictionaryService = dictionaryService;
    }

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ViewDictionaryListResponse<>))]
    public virtual async Task<IActionResult> ViewAsList(
        [FromQuery] ViewDictionaryListRequest request,
        CancellationToken cancellationToken)
    {
        var result = await DictionaryService.ViewAsList(
            request.PageRequest,
            request.Code,
            request.Name,
            request.Description,
            request.FullMatching,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = nameof(AuthorizationPolicies.RequireAdminRole))]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public virtual async Task<IActionResult> Upsert(
        [FromBody] CreateDictionaryItemRequest<TDictionaryDto> request)
    {
        await DictionaryService.Upsert(request.DictionaryDto);

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = nameof(AuthorizationPolicies.RequireAdminRole))]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public virtual async Task<IActionResult> Delete(string id)
    {
        await DictionaryService.Delete(id);

        return Ok();
    }
}