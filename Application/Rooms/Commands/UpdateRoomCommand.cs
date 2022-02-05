using AutoMapper;
using HotelAutomationApp.Application.File.Models;
using HotelAutomationApp.Domain.Models.Rooms;
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
            ICollection<ImageDto> images)
        {
            Id = id;
            MaxGuestsCount = maxGuestsCount;
            Capacity = capacity;
            PricePerNight = pricePerNight;
            IsAvailable = isAvailable;
            RoomGroupId = roomGroupId;
            Images = images;
        }

        public string Id { get; set; }
        public int MaxGuestsCount { get; set; }
        public double Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public string RoomGroupId { get; set; }
        public ICollection<ImageDto> Images { get; set; }

        private class Handler : AsyncRequestHandler<UpdateRoomCommand>
        {
            private readonly IDbContext _db;
            private readonly IMapper _mapper;
            private readonly ISecurityContext _securityContext;

            public Handler(IDbContext db, IMapper mapper, ISecurityContext securityContext)
            {
                _db = db;
                _mapper = mapper;
                _securityContext = securityContext;
            }

            protected override async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
            {
                var room = await _db.Rooms.FindAsync(request.Id);

                room!.MaxGuestsCount = request.MaxGuestsCount;
                room.Capacity = request.Capacity;
                room.PricePerNight = request.PricePerNight;
                room.IsAvailable = request.IsAvailable;
                room.RoomGroupId = request.RoomGroupId;
                room.LastModifiedBy = _securityContext.UserId;

                var images = await _db.RoomImages.Where(q => q.RoomId == request.Id).ToListAsync(cancellationToken);
                _db.RoomImages.RemoveRange(images);

                room.Images = request.Images
                    .Select(image => new RoomImage(image.FileName, image.FileType, image.Data, room.Id, room)).ToList();

                _db.Rooms.Update(room);

                await _db.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}