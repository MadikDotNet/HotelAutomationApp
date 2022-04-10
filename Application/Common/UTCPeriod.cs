using HotelAutomationApp.Shared.Common.Abstractions;

namespace HotelAutomationApp.Application.Common;

public class UTCPeriod : IPeriod
{
    private DateTime _dateFrom;
    private DateTime _dateTo;

    public UTCPeriod(DateTime dateFrom, DateTime dateTo)
    {
        DateFrom = dateFrom;
        DateTo = dateTo;
    }

    public UTCPeriod()
    {
        
    }
    
    public DateTime DateFrom
    {
        get => _dateFrom;
        set => _dateFrom = value.ToUniversalTime();
    }

    public DateTime DateTo
    {
        get => _dateTo;
        set => _dateTo = value.ToUniversalTime();
    }
}