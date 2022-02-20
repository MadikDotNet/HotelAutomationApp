using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.MediaFiles;
using HotelAutomationApp.Domain.Models.RoomGroupServices;
using HotelAutomationApp.Domain.Models.ValueObjects;

namespace HotelAutomationApp.Domain.Models.RoomGroups
{
    public class RoomGroup : BaseDictionary
    {
        public RoomGroup(
            string name,
            string code,
            string description,
            Price minPrice) : base(name, code, description)
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

        [ForeignKey(nameof(Media))]
        public string MediaId { get; set; }
        public Media Media { get; set; }

        /// <summary>
        /// Available service for entire room group
        /// </summary>
        public ICollection<RoomGroupService> RoomGroupServices { get; set; }
    }
}