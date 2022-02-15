namespace HotelAutomationApp.Shared.Extensions;

public static class ValidationExtensions
{
    public static T EnsureIsNotNull<T>(this T? value)
        where T : class =>
        value ?? throw new ArgumentException($"{nameof(value)} cannot be null");

    public static T EnsureIsNotNull<T>(this T? value, string paramName)
        where T : class =>
        value.EnsureIsNotNull($"{nameof(value)} cannot be null", paramName);

    public static T EnsureIsNotNull<T>(this T? value, string message, string paramName)
        where T : class =>
        value ?? throw new ArgumentException(message, paramName);

    public static string EnsureIsNotEmpty(this string value) =>
        string.IsNullOrWhiteSpace(value) || !value.Any() 
            ? throw new ArgumentException($"{nameof(value)} cannot be null or empty") 
            : value;

    public static string? EnsureIsNotEmpty(this string? value, string paramName) =>
        string.IsNullOrWhiteSpace(value) || !value.Any()
            ? throw new ArgumentException($"{nameof(value)} cannot be null or empty", paramName)
            : value;
}