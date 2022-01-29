using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.ValueObjects;

namespace HotelAutomationApp.Domain.Models.Rooms
{
    public class RoomGroup : BaseDictionary<RoomGroup>
    {
        public RoomGroup(
            string name,
            string code,
            string description,
            string parentId,
            ICollection<RoomGroup> children,
            Price minPrice) : base(name, code, description, parentId, children)
        {
            MinPrice = minPrice;
        }

        public RoomGroup()
        {
            
        }
        
        /// <summary>
        /// Lower price range for the group
        /// </summary>
        public Price MinPrice { get; set; }
    }
}