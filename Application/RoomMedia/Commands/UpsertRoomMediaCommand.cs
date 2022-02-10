using AutoMapper;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Domain.Models.MediaFiles;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.RoomMedia.Commands;

public class UpsertRoomMediaCommand : IRequest
{
    public UpsertRoomMediaCommand(string roomId, ICollection<MediaDto> media)
    {
        RoomId = roomId;
        Media = media;
    }

    public string RoomId { get; }
    public ICollection<MediaDto> Media { get; }

    private class Handler : AsyncRequestHandler<UpsertRoomMediaCommand>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDb, IMapper mapper)
        {
            _applicationDb = applicationDb;
            _mapper = mapper;
        }

        protected override async Task Handle(UpsertRoomMediaCommand request, CancellationToken cancellationToken)
        {
            var room = await _applicationDb.Room.FindAsync(request.RoomId); 

            var newMedia = request.Media
                .ExcludeAfterFilter(q => !q.HasId, out var remain);

            var alreadyExistMedia = await _applicationDb.Media
                .Where(q => remain.Select(w => w.Id)
                    .Any(w => w == q.Id))
                .ToListAsync(CancellationToken.None);

            newMedia = remain
                .ExcludeSameElements(alreadyExistMedia, first => first.Id, second => second.Id)
                .Concat(newMedia).ToList();

            var mustBeAdded = alreadyExistMedia.Select(q => new Domain.Models.Rooms.RoomMedia(room.Id, q.Id))
                .Concat(newMedia.Select(q =>
                {
                    var @new = q with {Id = Guid.NewGuid().ToString()};
                    return new Domain.Models.Rooms.RoomMedia(room.Id, room, @new.Id, _mapper.Map<Media>(@new));
                })).ToList();

            _applicationDb.RoomMedia.AddRange(mustBeAdded);
            await _applicationDb.SaveChangesAsync(CancellationToken.None);
        }
    }
}