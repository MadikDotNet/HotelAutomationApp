namespace HotelAutomationApp.Application.Common.Pagination;

public class PageResponse<T>
{
    public PageResponse(int total, ICollection<T> entities)
    {
        Total = total;
        Entities = entities;
    }

    public PageResponse()
    {
        
    }

    public int Total { get; set; }
    public ICollection<T> Entities { get; set; }
}

