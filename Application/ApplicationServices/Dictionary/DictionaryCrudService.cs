using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common.Dictionary.Models;
using HotelAutomationApp.Application.Common.Extensions;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using HotelAutomationApp.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.ApplicationServices.Dictionary;

public class DictionaryCrudService<TDictionary, TDictionaryDto>
    where TDictionary : BaseDictionary<TDictionary>
    where TDictionaryDto : BaseDictionaryDto
{
    private readonly IDbContext _db;
    private readonly IMapper _mapper;
    private readonly ISecurityContext _securityContext;

    public DictionaryCrudService(IDbContext db, IMapper mapper, ISecurityContext securityContext)
    {
        _db = db;
        _mapper = mapper;
        _securityContext = securityContext;
    }

    public virtual async Task<PageResponse<TDictionaryDto>> ViewAsList(
        PageRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _db.AsQueryable<TDictionary>()
            .ProjectTo<TDictionaryDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return result.AsPageResponse(request);
    }


    public virtual async Task Create(TDictionaryDto dictionaryDto)
    {
        dictionaryDto.Code.EnsureIsNotEmpty(nameof(dictionaryDto.Code));
        dictionaryDto.Name.EnsureIsNotEmpty(nameof(dictionaryDto.Name));

        if (!string.IsNullOrEmpty(dictionaryDto.ParentId) &&
            !await _db.AsQueryable<TDictionary>().AnyAsync(entity => entity.Id == dictionaryDto.ParentId))
        {
            throw new ArgumentException($"Parent with id {dictionaryDto.ParentId} not found");
        }

        var record = dictionaryDto with {Id = Guid.NewGuid().ToString()};

        var newEntity = _mapper.Map<TDictionary>(record);

        _db.AsDbSet<TDictionary>().Add(newEntity);
        await _db.SaveChangesAsync(CancellationToken.None);
    }

    public virtual async Task Update(TDictionaryDto dictionaryDto)
    {
        dictionaryDto.Code.EnsureIsNotEmpty(nameof(dictionaryDto.Code));
        dictionaryDto.Name.EnsureIsNotEmpty(nameof(dictionaryDto.Name));
        dictionaryDto.Id.EnsureIsNotEmpty(nameof(dictionaryDto.Id));

        if (!await _db.AsQueryable<TDictionary>().AnyAsync(entity => entity.Id == dictionaryDto.Id))
        {
            throw new ArgumentException($"Dictionary item with id {dictionaryDto.Id} not found");
        }

        if (!string.IsNullOrEmpty(dictionaryDto.ParentId) &&
            !await _db.AsQueryable<TDictionary>().AnyAsync(entity => entity.Id == dictionaryDto.ParentId))
        {
            throw new ArgumentException($"Parent with id {dictionaryDto.ParentId} not found");
        }

        var dictionaryEntity = _mapper.Map<TDictionary>(dictionaryDto);
        
        _db.AsDbSet<TDictionary>().Update(dictionaryEntity);
        await _db.SaveChangesAsync(CancellationToken.None);
    }

    public virtual async Task Delete(string id)
    {
        id.EnsureIsNotEmpty(nameof(id));

        var dictionaryItem = await _db
            .AsQueryable<TDictionary>()
            .FirstOrDefaultAsync(q => q.Id == id, CancellationToken.None);

        if (dictionaryItem is null)
        {
            throw new ArgumentException("Dictionary item not found");
        }

        _db.AsDbSet<TDictionary>().Remove(dictionaryItem);

        await _db.SaveChangesAsync(CancellationToken.None);
    }
}