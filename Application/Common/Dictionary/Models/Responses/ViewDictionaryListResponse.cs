using HotelAutomationApp.Application.Common.Pagination;

namespace HotelAutomationApp.Application.Common.Dictionary.Models.Responses;

public class ViewDictionaryListResponse<TDictionaryDto>
    where TDictionaryDto : BaseDictionaryDto
{
    public PageResponse<TDictionaryDto> PageResponse { get; set; }
}