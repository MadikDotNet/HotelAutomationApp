namespace HotelAutomationApp.Application.Common.Dictionary.Models.Requests;

public class UpdateDictionaryItemRequest<TDictionaryDto>
{
    public TDictionaryDto DictionaryDto { get; set; }
}