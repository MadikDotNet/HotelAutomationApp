using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common.Dictionary.Models;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Common.Abstractions;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.ApplicationServices.Dictionary;

public class TreeDictionaryCrudService<TTreeDictionary, TTreeDictionaryDto> :
    DictionaryCrudService<TTreeDictionary, TTreeDictionaryDto>
    where TTreeDictionary : BaseDictionary, ITreeDictionary
    where TTreeDictionaryDto : TreeDictionaryDto<TTreeDictionaryDto>
{
    public TreeDictionaryCrudService(IApplicationDbContext applicationDb, IMapper mapper) : base(applicationDb, mapper)
    {
    }

    public async Task<ICollection<TTreeDictionaryDto>> ViewAsTree(CancellationToken cancellationToken) =>
        (await ApplicationDb.AsDbSet<TTreeDictionary>()
            .ProjectTo<TTreeDictionaryDto>(Mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken))
        .AsRecursiveTree(parent => parent.Id, child => child.ParentId).ToList();

    public override async Task Create(TTreeDictionaryDto dictionaryDto)
    {
        await EnsureIsValid(dictionaryDto);
        await base.Create(dictionaryDto);
    }

    public override async Task Update(TTreeDictionaryDto dictionaryDto)
    {
        await EnsureIsValid(dictionaryDto);
        await base.Update(dictionaryDto);
    }

    private async Task EnsureIsValid(TTreeDictionaryDto dictionaryDto)
    {
        if (!string.IsNullOrEmpty(dictionaryDto.ParentId) &&
            !await ApplicationDb.AsDbSet<TTreeDictionary>().AnyAsync(entity => entity.Id == dictionaryDto.ParentId))
        {
            throw new ArgumentException($"Parent with id {dictionaryDto.ParentId} not found");
        }
    }
}