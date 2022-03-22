using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common.Dictionary.Models;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Extensions;
using HotelAutomationApp.Application.Extensions.IQueryable;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.ApplicationServices.Dictionary;

public class DictionaryCrudService<TDictionary, TDictionaryDto>
    where TDictionary : BaseDictionary
    where TDictionaryDto : BaseDictionaryDto
{
    protected readonly IApplicationDbContext ApplicationDb;
    protected readonly IMapper Mapper;

    public DictionaryCrudService(IApplicationDbContext applicationDb, IMapper mapper)
    {
        ApplicationDb = applicationDb;
        Mapper = mapper;
    }

    public virtual async Task<PageResponse<TDictionaryDto>> ViewAsList(
        PageRequest? request,
        string? code,
        string? name,
        string? description,
        bool fullMatching,
        CancellationToken cancellationToken)
    {
        var result = ApplicationDb.AsDbSet<TDictionary>().AsQueryable();

        return (await result.Filter(code, name, description, fullMatching)
                .ProjectTo<TDictionaryDto>(Mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken))
            .AsPageResponse(request);
    }

    public virtual async Task Upsert(TDictionaryDto dictionaryDto)
    {
        dictionaryDto.Name.EnsureIsNotEmpty(nameof(dictionaryDto.Name));

        var dbSet = ApplicationDb.AsDbSet<TDictionary>();
        
        if (!string.IsNullOrEmpty(dictionaryDto.Id) && await dbSet.AnyAsync(q => q.Id == dictionaryDto.Id))
        {
            dbSet.Update(Mapper.Map<TDictionary>(dictionaryDto));
            await ApplicationDb.SaveChangesAsync(CancellationToken.None);
            
            return;
        }
        
        var record = dictionaryDto with {Id = Guid.NewGuid().ToString()};

        var newEntity = Mapper.Map<TDictionary>(record);

        ApplicationDb.AsDbSet<TDictionary>().Add(newEntity);
        await ApplicationDb.SaveChangesAsync(CancellationToken.None);
    }

    public virtual async Task Delete(string id)
    {
        id.EnsureIsNotEmpty(nameof(id));

        var dictionaryItem = await ApplicationDb
            .AsDbSet<TDictionary>()
            .FirstOrDefaultAsync(q => q.Id == id, CancellationToken.None);

        if (dictionaryItem is null)
        {
            throw new ArgumentException("Dictionary item not found");
        }

        ApplicationDb.AsDbSet<TDictionary>().Remove(dictionaryItem);

        await ApplicationDb.SaveChangesAsync(CancellationToken.None);
    }
}