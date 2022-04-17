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

    public static IEnumerable<TEntity> Localize<TEntity>(this IEnumerable<TEntity> source)
        where TEntity : IAuditable =>
        source.Select(auditable =>
        {
            auditable.CreationDate = auditable.CreationDate.ToLocalTime();
            auditable.LastModifiedDate = auditable.LastModifiedDate.ToLocalTime();

            return auditable;
        });

    public static IEnumerable<TEntity> LocalizeByCreatedDate<TEntity>(this IEnumerable<TEntity> source)
        where TEntity : ICreateAuditor =>
        source.Select(auditable =>
        {
            auditable.CreationDate = auditable.CreationDate.ToLocalTime();

            return auditable;
        });

    public static IEnumerable<TEntity> LocalizeByUpdatedDate<TEntity>(this IEnumerable<TEntity> source)
        where TEntity : IUpdateAuditor =>
        source.Select(auditable =>
        {
            auditable.LastModifiedDate = auditable.LastModifiedDate.ToLocalTime();

            return auditable;
        });
}