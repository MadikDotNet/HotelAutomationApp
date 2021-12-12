using System;

namespace HotelAutomation.Shared
{
    public static class ExceptionExtensions
    {
        public static T ThrowIfArgNull<T>(this T value, string paramName)
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName);
            }

            return value;
        }

        public static string ThrowIfArgNullOrWhiteSpace(this string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{paramName} cannot be empty", paramName);
            }

            return value;
        }
    }
}