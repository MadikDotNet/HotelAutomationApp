namespace HotelAutomationApp.Shared.Common.Abstractions;

public interface IPeriod
{
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}