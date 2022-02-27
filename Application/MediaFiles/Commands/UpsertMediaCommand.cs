using AutoMapper;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Domain.Models.MediaFiles;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Extensions;
using MediatR;

namespace HotelAutomationApp.Application.MediaFiles.Commands;

public class UpsertMediaCommand : IRequest<ICollection<string>>
{
    public UpsertMediaCommand(ICollection<MediaDto> media)
    {
        Media = media;
    }

    public ICollection<MediaDto> Media { get; }

    private class Handler : IRequestHandler<UpsertMediaCommand, ICollection<string>>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDb, IMapper mapper)
        {
            _applicationDb = applicationDb;
            _mapper = mapper;
        }


        public async Task<ICollection<string>> Handle(UpsertMediaCommand request, CancellationToken cancellationToken)
        {
            var newMedia = request.Media.ExcludeAfterFilter(q => !q.HasId, out var remain)
                .Select(q => _mapper.Map<Media>(q with {Id = Guid.NewGuid().ToString()}))
                .ToList();

            var media = remain.Select(q => _mapper.Map<Media>(q)).ToList();

            _applicationDb.Media.UpdateRange(media);
            _applicationDb.Media.AddRange(newMedia);
            
            await _applicationDb.SaveChangesAsync(cancellationToken);

            return media.Concat(newMedia).Select(q => q.Id).ToList();
        }
    }
}