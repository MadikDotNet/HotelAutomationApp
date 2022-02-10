using AutoMapper;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;

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
            string roomGroupId)
        {
            Id = id;
            MaxGuestsCount = maxGuestsCount;
            Capacity = capacity;
            PricePerNight = pricePerNight;
            IsAvailable = isAvailable;
            RoomGroupId = roomGroupId;
        }

        public string Id { get; }
        public int MaxGuestsCount { get;  }
        public double Capacity { get;  }
        public decimal PricePerNight { get;  }
        public bool IsAvailable { get;  }
        public string RoomGroupId { get;  }

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

                _applicationDb.Room.Update(room);

                await _applicationDb.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}