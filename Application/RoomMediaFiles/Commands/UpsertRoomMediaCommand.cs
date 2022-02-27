using AutoMapper;
using HotelAutomationApp.Application.MediaFiles.Commands;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Domain.Models.RoomMediaFiles;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;

namespace HotelAutomationApp.Application.RoomMediaFiles.Commands;

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
        private readonly IMediator _mediator;

        public Handler(IApplicationDbContext applicationDb, IMediator mediator)
        {
            _applicationDb = applicationDb;
            _mediator = mediator;
        }

        protected override async Task Handle(UpsertRoomMediaCommand request, CancellationToken cancellationToken)
        {
            var room = await _applicationDb.Room.FindAsync(request.RoomId);

            var mediaIds = await _mediator.Send(new UpsertMediaCommand(request.Media), cancellationToken);

            var newRoomMedia = (from media in mediaIds
                join mf in _applicationDb.Media on media equals mf.Id
                join roomMedia in _applicationDb.RoomMedia on room.Id equals roomMedia.RoomId into rmGp
                where rmGp.All(q => q.MediaId != mf.Id)
                select new RoomMedia(Guid.NewGuid().ToString(), room.Id, mf.Id))
                .ToList();

            _applicationDb.RoomMedia.AddRange(newRoomMedia);
            await _applicationDb.SaveChangesAsync(CancellationToken.None);
        }
    }
}