using HotelAutomationApp.Domain.Models.ValueObjects;

namespace HotelAutomationApp.Domain.Common
{
    public class BaseDictionary : BaseEntity
    {
        public BaseDictionary(
            string name,
            string code,
            string description)
        {
            Name = name;
            Code = code;
            Description = description;
        }
        
        public BaseDictionary()
        {
            
        }
        
        public Name Name { get; set; }
        public string? Code { get; set; }
        public string Description { get; set; }
    }
}