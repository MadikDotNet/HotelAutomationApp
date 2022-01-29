using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomation.Domain.Models.ValueObjects;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.ValueObjects;

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
            Price pricePerNight,
            bool isAvailable) : base(id, createdBy, creationDate, lastModifiedBy, lastModifiedDate)
        {
            RoomGroupId = roomGroupId;
            MaxGuestsCount = maxGuestsCount;
            Capacity = capacity;
            PricePerNight = pricePerNight;
            IsAvailable = isAvailable;
        }

        /// <summary>
        /// The room is designed for this number of people
        /// </summary>
        public int MaxGuestsCount { get; set; }

        /// <summary>
        /// Info about capacity of room
        /// </summary>
        public double Capacity
        {
            get => _capacity;
            set => _capacity = value <= 0 ? throw new ArgumentException("Capacity cannot be 0 or less than 0") : value;
        }

        /// <summary>
        /// Price for one night
        /// </summary>
        public Price PricePerNight { get; set; }

        /// <summary>
        /// Room is available for this moment
        /// </summary>
        public bool IsAvailable { get; set; }
        
        /// <summary>
        /// Room group identifier
        /// </summary>
        [ForeignKey(nameof(RoomGroup))]
        public string RoomGroupId { get; set; }
        
        /// <summary>
        /// Group of rooms
        /// </summary>
        public RoomGroup RoomGroup { get; set; }

        /// <summary>
        /// Rooms images
        /// </summary>
        public ICollection<RoomImage> Images { get; set; }

        public static Room New(
            string creatorId,
            string roomGroupId,
            int maxQuestsCount,
            double capacity,
            Price pricePerNight) =>
            new Room(
                Guid.NewGuid().ToString(),
                creatorId,
                DateTime.UtcNow,
                creatorId,
                DateTime.UtcNow,
                roomGroupId,
                maxQuestsCount,
                capacity,
                pricePerNight,
                true);
    }
}