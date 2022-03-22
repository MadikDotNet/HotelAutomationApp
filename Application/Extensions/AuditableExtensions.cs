using HotelAutomationApp.Domain.Common.Abstractions.Audition;

namespace HotelAutomationApp.Application.Extensions;

public static class AuditableExtensions
{
    public static IAuditable ModifiedBy(this IAuditable auditable, string modifierId)
    {
        auditable.LastModifiedBy = modifierId;
        return auditable;
    }
    
    public static IAuditable CreatedBy(this IAuditable auditable, string creatorId)
    {
        auditable.CreatedBy = creatorId;
        return auditable;
    }
}