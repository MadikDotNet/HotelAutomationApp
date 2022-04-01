namespace HotelAutomationApp.Infrastructure.Interfaces.Security.Models;

public class Hash
{
    public Hash(string? value)
    {
        Value = value ?? throw new ArgumentException("Hash cannot be null or white space");
    }

    public readonly string Value;

    public static implicit operator string(Hash hash)
    {
        return hash.Value;
    }

    public static implicit operator Hash(string value)
    {
        return new Hash(value);
    }
}