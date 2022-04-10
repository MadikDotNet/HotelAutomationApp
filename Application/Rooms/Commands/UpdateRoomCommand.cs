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
            decimal pricePerHour,
            bool isAvailable,
            string roomGroupId)
        {
            Id = id;
            MaxGuestsCount = maxGuestsCount;
            Capacity = capacity;
            PricePerHour = pricePerHour;
            IsAvailable = isAvailable;
            RoomGroupId = roomGroupId;
        }

        public string Id { get; }
        public int MaxGuestsCount { get;  }
        public double Capacity { get;  }
        public decimal PricePerHour { get;  }
        public bool IsAvailable { get;  }
        public string RoomGroupId { get;  }

        private class Handler : AsyncRequestHandler<UpdateRoomCommand>
        {
            private readonly IApplicationDbContext _applicationDb;
            private readonly ISecurityContext _securityContext;

            public Handler(IApplicationDbContext applicationDb, ISecurityContext securityContext)
            {
                _applicationDb = applicationDb;
                _securityContext = securityContext;
            }

            protected override async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
            {
                var room = await _applicationDb.Room.FindAsync(request.Id);

                room!.MaxGuestsCount = request.MaxGuestsCount;
                room.Capacity = request.Capacity;
                room.PricePerHour = request.PricePerHour;
                room.RoomGroupId = request.RoomGroupId;
                room.LastModifiedBy = _securityContext.UserId;

                await _applicationDb.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}