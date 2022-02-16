using HotelAutomationApp.Application.Common;

namespace HotelAutomationApp.Application.Extensions
{
    public static class DistanceExtensions
    {
        public static bool IsInDistance(this double value, Distance<double>? distance) =>
            (distance?.From == null || value >= distance.From) &&
            (distance?.To == null || value <= distance.To);

        public static bool IsInDistance(this int value, Distance<int> distance) =>
            (distance?.From == null || value >= distance.From) &&
            (distance?.To == null || value <= distance.To);
        
        public static bool IsInDistance(this decimal value, Distance<decimal> distance) =>
            (distance?.From == null || value >= distance.From) &&
            (distance?.To == null || value <= distance.To);
    }
}