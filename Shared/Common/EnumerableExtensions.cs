using System.Collections.Generic;

namespace HotelAutomationApp.Shared.Common
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Yield<T>(this T value)
        {
            yield return value;
        }

        public static T[] YieldArray<T>(this T value) =>
            new[] {value};

        public static object[] YieldObjectArray(this object value) =>
            new object[] {value};
    }
}