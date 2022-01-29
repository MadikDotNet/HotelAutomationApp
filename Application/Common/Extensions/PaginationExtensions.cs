using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Common.Extensions;

public static class PaginationExtensions
{
    public static async Task<PageResponse<TEntity>> AsPageResponse<TEntity>(
        this IQueryable<TEntity> query,
        PageRequest pageRequest,
        CancellationToken cancellationToken) 
    where TEntity : BaseEntity
    {
        var count = await query.CountAsync(cancellationToken);
        
        var entities = await query
            .Skip(pageRequest.PageIndex * pageRequest.PageSize)
            .Take(pageRequest.PageSize)
            .ToListAsync(cancellationToken);

        return new PageResponse<TEntity>(count, entities);
    }
    
    public static PageResponse<T> AsPageResponse<T>(
        this IEnumerable<T> source,
        PageRequest pageRequest) 
    {
        var list = source as T[] ?? source.ToArray();
        
        var entities = list
            .Skip(pageRequest.PageIndex * pageRequest.PageSize)
            .Take(pageRequest.PageSize)
            .ToList();

        return new PageResponse<T>(list.Length, entities);
    }
}