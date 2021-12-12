using System;
using HotelAutomation.Domain.Models.ValueObjects;
using HotelAutomationApp.Domain.Common;

namespace HotelAutomation.Domain.Models.Rooms
{
    public class RoomGroup : AuditableEntity
    {
        private RoomGroup()
        {
        }

        public RoomGroup(
            string id,
            string createdBy,
            DateTime creationDate,
            string lastModifiedBy,
            DateTime lastModifiedDate,
            Name name,
            string description,
            Price minPrice) : base(id, createdBy, creationDate, lastModifiedBy, lastModifiedDate)
        {
            Name = name;
            Description = description;
            MinPrice = minPrice;
        }
        
        /// <summary>
        /// Room group name
        /// </summary>
        public Name Name { get; set; }
        
        /// <summary>
        /// Room group description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Lower price range for the group
        /// </summary>
        public Price MinPrice { get; set; }

        public static RoomGroup New(string creatorId, Name name, string description, Price minPrice) =>
            new RoomGroup(
                Guid.NewGuid().ToString(),
                creatorId,
                DateTime.UtcNow,
                creatorId,
                DateTime.UtcNow,
                name,
                description,
                minPrice);
    }
}