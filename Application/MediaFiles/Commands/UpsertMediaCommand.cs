using AutoMapper;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Domain.Models.MediaFiles;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.MediaFiles.Commands;

public class UpsertMediaCommand : IRequest
{
    public UpsertMediaCommand(ICollection<MediaDto> media)
    {
        Media = media;
    }

    public ICollection<MediaDto> Media { get; }

    private class Handler : AsyncRequestHandler<UpsertMediaCommand>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDb, IMapper mapper)
        {
            _applicationDb = applicationDb;
            _mapper = mapper;
        }

        // TODO rewrite
        protected override async Task Handle(UpsertMediaCommand request, CancellationToken cancellationToken)
        {
            var newMedia = request.Media.ExcludeAfterFilter(q => !q.HasId, out var remind);

            var alreadyExistMedia = (await _applicationDb.Media
                    .Where(q => remind.Select(w => w.Id).Contains(q.Id))
                    .AsNoTracking()
                    .ToListAsync(CancellationToken.None))
                .Join(request.Media, q => q.Id, w => w.Id, (left, right) => _mapper.Map<Media>(right))
                .ToList();

            var mustBeAdded = remind.ExcludeSameElements(alreadyExistMedia, q => q.Id, w => w.Id)
                .Concat(newMedia)
                .Select(q => _mapper.Map<Media>(q with {Id = Guid.NewGuid().ToString()}))
                .ToList();

            _applicationDb.Media.AddRange(mustBeAdded);
            _applicationDb.Media.UpdateRange(alreadyExistMedia);
            await _applicationDb.SaveChangesAsync(cancellationToken);
        }
    }
}