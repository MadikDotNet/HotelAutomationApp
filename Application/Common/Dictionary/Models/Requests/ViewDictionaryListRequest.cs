using HotelAutomationApp.Application.Common.Pagination;

namespace HotelAutomationApp.Application.Common.Dictionary.Models.Requests;

public class ViewDictionaryListRequest
{
    public PageRequest PageRequest { get; set; }
    public bool ShowDeleted { get; set; }
}
