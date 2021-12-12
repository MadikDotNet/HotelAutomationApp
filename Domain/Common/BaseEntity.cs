namespace HotelAutomation.Domain.Common
{
    public class BaseEntity
    {
        public BaseEntity()
        {
        }
        
        public BaseEntity(string id) =>
            Id = id;

        public string Id { get; set; }
    }
}