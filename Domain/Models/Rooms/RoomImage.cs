using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomation.Domain.Common;

namespace HotelAutomation.Domain.Models.Rooms
{
    public class RoomImage : Image
    {
        /// <summary>
        /// Owner room id 
        /// </summary>
        [ForeignKey(nameof(Room))]
        public string RoomId { get; set; }
        
        /// <summary>
        /// Owner room
        /// </summary>
        public Room Room { get; set; }
    }
}