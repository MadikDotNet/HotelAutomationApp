using System;

namespace HotelAutomation.Domain.Models.ValueObjects
{
    public class Name
    {
        public Name(string value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), "Name cannot be null");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name cannot be empty or whitespace");
            }

            Value = value;
        }

        public string Value { get; private set; }

        public static implicit operator string(Name name) => name.Value;

        public static implicit operator Name(string value) => new Name(value);
    }
}