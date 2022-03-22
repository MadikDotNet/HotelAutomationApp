using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.MediaFiles;
using HotelAutomationApp.Domain.Models.Rooms;

namespace HotelAutomationApp.Domain.Models.RoomMediaFiles
{
    /// <summary>
    /// Link table between room and fileMetadata
    /// </summary>
    public class RoomFile : BaseEntity
    {
        public RoomFile()
        {
        }

        public RoomFile(string id, string roomId, string fileMetadataId)
        {
            Id = id;
            RoomId = roomId;
            FileMetadataId = fileMetadataId;
        }

        public RoomFile(
            string id,
            string roomId,
            Room room,
            string fileMetadataId,
            FileMetadata fileMetadata)
        {
            Id = id;
            RoomId = roomId;
            Room = room;
            FileMetadataId = fileMetadataId;
            FileMetadata = fileMetadata;
        }

        [ForeignKey(nameof(Room))] 
        public string RoomId { get; set; }
        public Room Room { get; set; }
        [ForeignKey(nameof(FileMetadata))] 
        public string FileMetadataId { get; set; }
        public FileMetadata FileMetadata { get; set; }
    }
}