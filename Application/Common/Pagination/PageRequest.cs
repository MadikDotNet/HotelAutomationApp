namespace HotelAutomationApp.Application.Common.Pagination;

public class PageRequest
{
    public PageRequest()
    {
    }

    public PageRequest(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    private int _pageIndex;

    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = value < 0 ? throw new ArgumentException($"{PageIndex} cannot be less than 0") : value;
    }

    private int _pageSize;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 0 ? throw new ArgumentException($"{PageSize} cannot be less than 0") : value;
    }
}