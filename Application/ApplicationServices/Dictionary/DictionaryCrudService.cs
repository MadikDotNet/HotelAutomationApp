using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common.Dictionary.Models;
using HotelAutomationApp.Application.Common.Extensions;
using HotelAutomationApp.Application.Common.Pagination;
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
        PageRequest request,
        CancellationToken cancellationToken)
    {
        var result = await ApplicationDb.AsDbSet<TDictionary>()
            .ProjectTo<TDictionaryDto>(Mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return result.AsPageResponse(request);
    }

    public virtual async Task Create(TDictionaryDto dictionaryDto)
    {
        dictionaryDto.Code.EnsureIsNotEmpty(nameof(dictionaryDto.Code));
        dictionaryDto.Name.EnsureIsNotEmpty(nameof(dictionaryDto.Name));

        var record = dictionaryDto with {Id = Guid.NewGuid().ToString()};

        var newEntity = Mapper.Map<TDictionary>(record);

        ApplicationDb.AsDbSet<TDictionary>().Add(newEntity);
        await ApplicationDb.SaveChangesAsync(CancellationToken.None);
    }

    public virtual async Task Update(TDictionaryDto dictionaryDto)
    {
        dictionaryDto.Code.EnsureIsNotEmpty(nameof(dictionaryDto.Code));
        dictionaryDto.Name.EnsureIsNotEmpty(nameof(dictionaryDto.Name));
        dictionaryDto.Id.EnsureIsNotEmpty(nameof(dictionaryDto.Id));

        if (!await ApplicationDb.AsDbSet<TDictionary>().AnyAsync(entity => entity.Id == dictionaryDto.Id))
        {
            throw new ArgumentException($"Dictionary item with id {dictionaryDto.Id} not found");
        }

        var dictionaryEntity = Mapper.Map<TDictionary>(dictionaryDto);

        ApplicationDb.AsDbSet<TDictionary>().Update(dictionaryEntity);
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