using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.MediaFiles;

namespace HotelAutomationApp.Domain.Models.Rooms
{
    /// <summary>
    /// Link table between room and media
    /// </summary>
    public class RoomMedia : BaseEntity
    {
        public RoomMedia()
        {
            
        }

        public RoomMedia(string roomId, string mediaId)
        {
            RoomId = roomId;
            MediaId = mediaId;
        }
        
        public RoomMedia(
            string roomId,
            Room room,
            string mediaId,
            Media media)
        {
            RoomId = roomId;
            Room = room;
            MediaId = mediaId;
            Media = media;
        }

        [ForeignKey(nameof(Room))]
        public string RoomId { get; set; }
        public Room Room { get; set; }
        [ForeignKey(nameof(Media))]
        public string MediaId { get; set; }
        public Media Media { get; set; }
    }
}