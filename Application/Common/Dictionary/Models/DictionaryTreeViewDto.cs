using HotelAutomationApp.Domain.Common.Abstractions;

namespace HotelAutomationApp.Application.Common.Dictionary.Models;

public record DictionaryTreeViewDto<TDictionaryDto> : BaseDictionaryDto, IRecursiveTree<TDictionaryDto>
{
    public TDictionaryDto Parent { get; set; }
    public ICollection<TDictionaryDto> Children { get; set; }
}