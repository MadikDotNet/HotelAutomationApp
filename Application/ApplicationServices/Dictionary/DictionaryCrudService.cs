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
    private readonly IApplicationDbContext _applicationDb;
    private readonly IMapper _mapper;

    public DictionaryCrudService(IApplicationDbContext applicationDb, IMapper mapper)
    {
        _applicationDb = applicationDb;
        _mapper = mapper;
    }

    public virtual async Task<PageResponse<TDictionaryDto>> ViewAsList(
        PageRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _applicationDb.AsQueryable<TDictionary>()
            .ProjectTo<TDictionaryDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return result.AsPageResponse(request);
    }

    public virtual async Task<ICollection<TTreeDictionaryDto>> ViewAsTree
        <TTreeDictionary, TTreeDictionaryDto>(CancellationToken cancellationToken)
        where TTreeDictionary : BaseEntity
        where TTreeDictionaryDto : TreeDictionaryDto<TTreeDictionaryDto> =>
        (await _applicationDb.AsQueryable<TTreeDictionary>()
            .ProjectTo<TTreeDictionaryDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken))
        .AsRecursiveTree(parent => parent.Id, child => child.ParentId).ToList();

    public virtual async Task Create(TDictionaryDto dictionaryDto)
    {
        dictionaryDto.Code.EnsureIsNotEmpty(nameof(dictionaryDto.Code));
        dictionaryDto.Name.EnsureIsNotEmpty(nameof(dictionaryDto.Name));

        if (!string.IsNullOrEmpty(dictionaryDto.ParentId) &&
            !await _applicationDb.AsQueryable<TDictionary>().AnyAsync(entity => entity.Id == dictionaryDto.ParentId))
        {
            throw new ArgumentException($"Parent with id {dictionaryDto.ParentId} not found");
        }

        var record = dictionaryDto with {Id = Guid.NewGuid().ToString()};

        var newEntity = _mapper.Map<TDictionary>(record);

        _applicationDb.AsDbSet<TDictionary>().Add(newEntity);
        await _applicationDb.SaveChangesAsync(CancellationToken.None);
    }

    public virtual async Task Update(TDictionaryDto dictionaryDto)
    {
        dictionaryDto.Code.EnsureIsNotEmpty(nameof(dictionaryDto.Code));
        dictionaryDto.Name.EnsureIsNotEmpty(nameof(dictionaryDto.Name));
        dictionaryDto.Id.EnsureIsNotEmpty(nameof(dictionaryDto.Id));

        if (!await _applicationDb.AsQueryable<TDictionary>().AnyAsync(entity => entity.Id == dictionaryDto.Id))
        {
            throw new ArgumentException($"Dictionary item with id {dictionaryDto.Id} not found");
        }

        if (!string.IsNullOrEmpty(dictionaryDto.ParentId) &&
            !await _applicationDb.AsQueryable<TDictionary>().AnyAsync(entity => entity.Id == dictionaryDto.ParentId))
        {
            throw new ArgumentException($"Parent with id {dictionaryDto.ParentId} not found");
        }

        var dictionaryEntity = _mapper.Map<TDictionary>(dictionaryDto);

        _applicationDb.AsDbSet<TDictionary>().Update(dictionaryEntity);
        await _applicationDb.SaveChangesAsync(CancellationToken.None);
    }

    public virtual async Task Delete(string id)
    {
        id.EnsureIsNotEmpty(nameof(id));

        var dictionaryItem = await _applicationDb
            .AsQueryable<TDictionary>()
            .FirstOrDefaultAsync(q => q.Id == id, CancellationToken.None);

        if (dictionaryItem is null)
        {
            throw new ArgumentException("Dictionary item not found");
        }

        _applicationDb.AsDbSet<TDictionary>().Remove(dictionaryItem);

        await _applicationDb.SaveChangesAsync(CancellationToken.None);
    }
}