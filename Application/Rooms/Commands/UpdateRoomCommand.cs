using AutoMapper;
using HotelAutomationApp.Application.File.Models;
using HotelAutomationApp.Domain.MediaFiles;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Rooms.Commands
{
    public class UpdateRoomCommand : IRequest
    {
        public UpdateRoomCommand(
            string id,
            int maxGuestsCount,
            double capacity,
            decimal pricePerNight,
            bool isAvailable,
            string roomGroupId,
            ICollection<MediaDto> media)
        {
            Id = id;
            MaxGuestsCount = maxGuestsCount;
            Capacity = capacity;
            PricePerNight = pricePerNight;
            IsAvailable = isAvailable;
            RoomGroupId = roomGroupId;
            Media = media;
        }

        public string Id { get; set; }
        public int MaxGuestsCount { get; set; }
        public double Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public string RoomGroupId { get; set; }
        public ICollection<MediaDto> Media { get; set; }

        private class Handler : AsyncRequestHandler<UpdateRoomCommand>
        {
            private readonly IApplicationDbContext _applicationDb;
            private readonly ISecurityContext _securityContext;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext applicationDb, ISecurityContext securityContext, IMapper mapper)
            {
                _applicationDb = applicationDb;
                _securityContext = securityContext;
                _mapper = mapper;
            }

            protected override async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
            {
                var room = await _applicationDb.Room.FindAsync(request.Id);

                room!.MaxGuestsCount = request.MaxGuestsCount;
                room.Capacity = request.Capacity;
                room.PricePerNight = request.PricePerNight;
                room.IsAvailable = request.IsAvailable;
                room.RoomGroupId = request.RoomGroupId;
                room.LastModifiedBy = _securityContext.UserId;

                var roomMedia = await _applicationDb.RoomMedia
                    .Where(q => q.RoomId == request.Id)
                    .ToListAsync(cancellationToken);

                _applicationDb.RoomMedia.RemoveRange(roomMedia);

                await _applicationDb.SaveChangesAsync(CancellationToken.None);

                var newMedia = request.Media.Where(q => !q.HasId);
                
                

                _applicationDb.Room.Update(room);

                await _applicationDb.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}