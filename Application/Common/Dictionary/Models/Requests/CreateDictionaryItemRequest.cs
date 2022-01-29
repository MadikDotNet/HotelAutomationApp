namespace HotelAutomationApp.Application.Common.Dictionary.Models.Requests;

public class CreateDictionaryItemRequest<TDictionary, TDictionaryDto>
{
    public TDictionaryDto DictionaryDto { get; set; }
}