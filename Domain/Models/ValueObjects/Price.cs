
namespace HotelAutomationApp.Domain.Models.ValueObjects
{
    public class Price
    {
        public Price(decimal value)
        {
            if (value < 0)
            {
                throw new AggregateException("Price cannot be less than 0");
            }
            
            Value = value;
        }

        public decimal Value { get; private set; }

        public static implicit operator decimal(Price price) => price.Value;
        
        public static implicit operator Price(decimal value) => new Price(value);
    }
}