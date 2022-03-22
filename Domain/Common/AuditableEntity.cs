using HotelAutomationApp.Domain.Common.Abstractions.Audition;

namespace HotelAutomationApp.Domain.Common
{
    /// <summary>
    /// Main class for auditable entities
    /// </summary>
    public class AuditableEntity : BaseEntity, IAuditable
    {
        public AuditableEntity()
        {
        }
        
        public AuditableEntity(
            string id,
            string createdBy,
            DateTime creationDate,
            string lastModifiedBy,
            DateTime lastModifiedDate) : base(id)
        {
            CreatedBy = createdBy;
            CreationDate = creationDate;
            LastModifiedBy = lastModifiedBy;
            LastModifiedDate = lastModifiedDate;
        }
        
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// State of entity
        /// </summary>
        public bool IsDeleted { get; set; }
        
        /// <summary>
        /// Who was remove
        /// </summary>
        public string? DeletedBy { get; set; }

        /// <summary>
        /// When was removed
        /// </summary>
        public DateTime? DeletedDate { get; set; }
    }
}