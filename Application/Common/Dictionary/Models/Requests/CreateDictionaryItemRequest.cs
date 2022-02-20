namespace HotelAutomationApp.Application.Common.Dictionary.Models.Requests;

public class CreateDictionaryItemRequest<TDictionaryDto>
{
    public TDictionaryDto DictionaryDto { get; set; }
}