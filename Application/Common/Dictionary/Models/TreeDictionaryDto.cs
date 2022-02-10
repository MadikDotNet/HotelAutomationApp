using HotelAutomationApp.Shared.Common.Abstractions;
using Newtonsoft.Json;

namespace HotelAutomationApp.Application.Common.Dictionary.Models;

public record TreeDictionaryDto<TTreeDictionaryDto> : BaseDictionaryDto, IRecursiveTree<TTreeDictionaryDto> 
    where TTreeDictionaryDto : class
{
    public string? ParentId { get; set; }
    
    [JsonIgnore]
    public TTreeDictionaryDto? Parent { get; set; }
    public ICollection<TTreeDictionaryDto> Children { get; set; }
    
    [JsonIgnore]
    public bool HasParent => !string.IsNullOrEmpty(ParentId);
}