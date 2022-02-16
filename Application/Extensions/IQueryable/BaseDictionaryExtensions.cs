using HotelAutomationApp.Domain.Common;

namespace HotelAutomationApp.Application.Extensions.IQueryable;

public static class BaseDictionaryExtensions
{
    public static IQueryable<TDictionary> Filter<TDictionary>(
        this IQueryable<TDictionary> source,
        string? code,
        string? name,
        string? description,
        bool fullMatching)
        where TDictionary : BaseDictionary
    {
        if (!string.IsNullOrEmpty(code))
        {
            source = source.Where(q => fullMatching ? q.Code == code : q.Code.Contains(code));
        }

        if (!string.IsNullOrEmpty(name))
        {
            source = source.Where(q => fullMatching ? q.Name == code : q.Code.Contains(name));
        }
        
        if (!string.IsNullOrEmpty(description))
        {
            source = source.Where(q => fullMatching ? q.Name == description : q.Code.Contains(description));
        }

        return source;
    }
}