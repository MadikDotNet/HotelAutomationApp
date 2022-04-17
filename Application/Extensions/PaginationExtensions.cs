using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Extensions;

public static class PaginationExtensions
{
    public static async Task<PageResponse<TEntity>> AsPageResponseAsync<TEntity>(
        this IQueryable<TEntity> query,
        PageRequest? pageRequest,
        CancellationToken cancellationToken)
        where TEntity : BaseEntity
    {
        var count = await query.CountAsync(cancellationToken);

        var entities = pageRequest is not null
            ? await query
                .Skip(pageRequest.PageIndex * pageRequest.PageSize)
                .Take(pageRequest.PageSize)
                .ToListAsync(cancellationToken)
            : await query.ToListAsync(cancellationToken);

        return new PageResponse<TEntity>(count, entities);
    }

    public static PageResponse<T> AsPageResponse<T>(
        this IEnumerable<T> source,
        PageRequest? pageRequest)
    {
        var list = source as T[] ?? source.ToArray();

        var entities = pageRequest is not null
            ? list
                .Skip(pageRequest.PageIndex * pageRequest.PageSize)
                .Take(pageRequest.PageSize)
                .ToList()
            : list.ToList();

        return new PageResponse<T>(list.Length, entities);
    }
}