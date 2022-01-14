using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomation.Domain.Models.Rooms;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.File;

namespace HotelAutomationApp.Domain.Models.Rooms
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