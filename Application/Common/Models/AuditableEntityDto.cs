using System;

namespace HotelAutomationApp.Application.Common.Models
{
    public class AuditableEntityDto
    {
        public string CreatedBy { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public string LastModifiedBy { get; set; }
        
        public DateTime LastModifiedDate { get; set; }

        public bool IsDeleted { get; set; }
        
        public string? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}