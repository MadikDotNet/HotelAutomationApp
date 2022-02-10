using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Application.ApplicationServices.Dictionary;
using HotelAutomationApp.Application.Common.Dictionary.Models;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Common.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace HotelAutomationApp.WebApi.Controllers.Common;

public abstract class DictionaryTreeController<TTreeDictionary, TTreeDictionaryDto, TDictionaryService> :
    DictionaryController<TTreeDictionary, TTreeDictionaryDto, TDictionaryService>
    where TTreeDictionary : BaseDictionary, ITreeDictionary, new()
    where TTreeDictionaryDto : TreeDictionaryDto<TTreeDictionaryDto>
    where TDictionaryService : DictionaryCrudService<TTreeDictionary, TTreeDictionaryDto>
{
    public DictionaryTreeController(TDictionaryService dictionaryService) : base(dictionaryService)
    {
    }

    [HttpGet]
    public virtual async Task<IActionResult> ViewAsTree(CancellationToken cancellationToken)
    {
        var result = await DictionaryService.ViewAsTree<TTreeDictionary, TTreeDictionaryDto>(cancellationToken);

        return Ok(result);
    }
}