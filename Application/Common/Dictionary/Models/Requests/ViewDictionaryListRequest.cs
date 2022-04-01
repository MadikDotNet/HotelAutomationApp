using HotelAutomationApp.Application.Common.Pagination;

namespace HotelAutomationApp.Application.Common.Dictionary.Models.Requests;

public class ViewDictionaryListRequest
{
    public PageRequest? PageRequest { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsAdditional { get; set; }
    public bool FullMatching { get; set; }
}