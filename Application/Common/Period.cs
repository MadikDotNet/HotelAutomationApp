using HotelAutomationApp.Shared.Common.Abstractions;

namespace HotelAutomationApp.Application.Common;

public class Period : IPeriod
{
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}