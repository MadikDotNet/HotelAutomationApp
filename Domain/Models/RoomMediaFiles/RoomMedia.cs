using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.MediaFiles;
using HotelAutomationApp.Domain.Models.Rooms;

namespace HotelAutomationApp.Domain.Models.RoomMediaFiles
{
    /// <summary>
    /// Link table between room and media
    /// </summary>
    public class RoomMedia : BaseEntity
    {
        public RoomMedia()
        {
        }

        public RoomMedia(string id, string roomId, string mediaId)
        {
            Id = id;
            RoomId = roomId;
            MediaId = mediaId;
        }

        public RoomMedia(
            string id,
            string roomId,
            Room room,
            string mediaId,
            Media media)
        {
            Id = id;
            RoomId = roomId;
            Room = room;
            MediaId = mediaId;
            Media = media;
        }

        [ForeignKey(nameof(Room))] public string RoomId { get; set; }
        public Room Room { get; set; }
        [ForeignKey(nameof(Media))] public string MediaId { get; set; }
        public Media Media { get; set; }
    }
}