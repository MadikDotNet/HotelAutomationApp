using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.RoomGroups;
using HotelAutomationApp.Domain.Models.RoomMediaFiles;

namespace HotelAutomationApp.Domain.Models.Rooms
{
    public class Room : AuditableEntity
    {
        private double _capacity;

        private Room()
        {
        }

        public Room(
            string id,
            string createdBy,
            DateTime creationDate,
            string lastModifiedBy,
            DateTime lastModifiedDate,
            string roomGroupId,
            int maxGuestsCount,
            double capacity,
            decimal pricePerHour,
            string description,
            string name) : base(id, createdBy, creationDate, lastModifiedBy, lastModifiedDate)
        {
            RoomGroupId = roomGroupId;
            MaxGuestsCount = maxGuestsCount;
            Capacity = capacity;
            PricePerHour = pricePerHour;
            Description = description;
            Name = name;
        }

        public int MaxGuestsCount { get; set; }

        public double Capacity
        {
            get => _capacity;
            set => _capacity = value <= 0 ? throw new ArgumentException("Capacity cannot be 0 or less than 0") : value;
        }

        public decimal PricePerHour { get; set; }

        [ForeignKey(nameof(RoomGroup))] public string RoomGroupId { get; set; }
        public RoomGroup RoomGroup { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RoomFile> RoomFiles { get; set; }

        public static Room New(
            string creatorId,
            string roomGroupId,
            int maxQuestsCount,
            double capacity,
            decimal pricePerHour,
            string description,
            string name) =>
            new Room(
                Guid.NewGuid().ToString(),
                creatorId,
                DateTime.UtcNow,
                creatorId,
                DateTime.UtcNow,
                roomGroupId,
                maxQuestsCount,
                capacity,
                pricePerHour,
                    description,
                name);
    }
}