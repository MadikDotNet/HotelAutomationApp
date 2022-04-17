using HotelAutomationApp.Domain.Common.Abstractions.Audition;
using HotelAutomationApp.Shared.Common.Abstractions;

namespace HotelAutomationApp.Application.Extensions.IQueryable;

public static class PeriodExtensions
{
    public static IQueryable<TEntity> CreatedBetween<TEntity>(this IQueryable<TEntity> source, IPeriod? period)
        where TEntity : ICreateAuditor =>
        source.Where(q => period == null ||
                          period.DateFrom == default ||
                          period.DateTo == default ||
                          q.CreationDate >= period.DateFrom &&
                          q.CreationDate <= period.DateTo);
}