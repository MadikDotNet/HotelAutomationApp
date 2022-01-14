namespace HotelAutomationApp.Application.Common
{
    public class Distance<T>
    {
        public Distance(T @from, T to)
        {
            From = @from;
            To = to;
        }

        public Distance()
        {
            
        }

        public T From { get; set; }
        public T To { get; set; }
    }
}