using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Application.ApplicationServices.Dictionary;
using HotelAutomationApp.Application.Common.Dictionary.Models;
using HotelAutomationApp.Application.Common.Dictionary.Models.Responses;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Common.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers.Common;

public class TreeDictionaryController<TTreeDictionary, TTreeDictionaryDto, TDictionaryService>
    : DictionaryController<TTreeDictionary, TTreeDictionaryDto, TDictionaryService>
    where TTreeDictionary : BaseDictionary, ITreeDictionary, new()
    where TTreeDictionaryDto : TreeDictionaryDto<TTreeDictionaryDto>
    where TDictionaryService : TreeDictionaryCrudService<TTreeDictionary, TTreeDictionaryDto>
{
    public TreeDictionaryController(TDictionaryService dictionaryService) : base(dictionaryService)
    {
    }

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ViewDictionaryListResponse<>))]
    public async Task<IActionResult> ViewAsTree(CancellationToken cancellationToken)
    {
        var result = await DictionaryService.ViewAsTree(cancellationToken);
        return Ok(result);
    }
}