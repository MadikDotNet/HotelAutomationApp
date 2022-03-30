namespace HotelAutomationApp.Shared.Common.Abstractions;

public interface IPeriod
{
    public DateTime DateFrom { get; }
    public DateTime DateTo { get; }
}