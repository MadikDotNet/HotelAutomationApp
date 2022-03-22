using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Rooms.Commands;
using HotelAutomationApp.Application.Rooms.Models;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;

namespace HotelAutomationApp.Application.Rooms.UseCases
{
    public class CreateRoomUseCase : TransactionUseCase<CreateRoomRequest>
    {
        private readonly IMediator _mediator;

        public CreateRoomUseCase(IApplicationDbContext applicationDb, IMediator mediator) : base(applicationDb)
        {
            _mediator = mediator;
        }

        protected override async Task HandleRequestAsync(CreateRoomRequest request, CancellationToken cancellationToken)
        {
            var createRoomCommand = new CreateRoomCommand(
                request.MaxGuestsCount,
                request.Capacity,
                request.PricePerNight,
                request.RoomGroupId,
                request.Files);
            
            await _mediator.Send(createRoomCommand, cancellationToken);
        }
    }

    public class CreateRoomRequest : IRequest
    {
        public int MaxGuestsCount { get; set; }
        public double Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public string RoomGroupId { get; set; }
        public ICollection<FileDto>? Files { get; set; }
    }
}