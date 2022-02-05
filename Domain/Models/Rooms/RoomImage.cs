using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.File;

namespace HotelAutomationApp.Domain.Models.Rooms
{
    public class RoomImage : Image
    {
        public RoomImage()
        {
            
        }

        public RoomImage(
            string fileName,
            string fileType,
            string data,
            string roomId,
            Room room) : base(fileName, fileType, data)
        {
            RoomId = roomId;
            Room = room;
        }

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