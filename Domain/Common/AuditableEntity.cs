using System;

namespace HotelAutomationApp.Domain.Common
{
    /// <summary>
    /// Main class for auditable entities
    /// </summary>
    public class AuditableEntity : BaseEntity
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
        
        /// <summary>
        /// Creator identifier 
        /// </summary>
        public string CreatedBy { get; set; }
        
        /// <summary>
        /// Date of creation
        /// </summary>
        public DateTime CreationDate { get; set; }
        
        /// <summary>
        /// Modifier identifier
        /// </summary>
        public string LastModifiedBy { get; set; }
        
        /// <summary>
        /// Date of last modify
        /// </summary>
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