using HotelAutomationApp.Shared.Common.Abstractions;
using Newtonsoft.Json;

namespace HotelAutomationApp.Application.Common.Dictionary.Models;

public record TreeDictionaryDto<TTreeDictionaryDto> : BaseDictionaryDto, IRecursiveTree<TTreeDictionaryDto> 
    where TTreeDictionaryDto : class
{
    private string? _parentId;
    
    public string? ParentId
    {
        get => _parentId;
        set => _parentId = string.IsNullOrEmpty(value) || !Guid.TryParse(value, out _) ? null : value;
    }
    
    public TTreeDictionaryDto? Parent { get; set; }
    public ICollection<TTreeDictionaryDto>? Children { get; set; }
    
    public bool HasParent => !string.IsNullOrEmpty(ParentId);
}