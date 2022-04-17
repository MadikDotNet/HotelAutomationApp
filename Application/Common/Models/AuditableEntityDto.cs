using System;
using HotelAutomationApp.Domain.Common.Abstractions.Audition;

namespace HotelAutomationApp.Application.Common.Models
{
    public record AuditableEntityDto : IAuditable
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