using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.File.Models;
using HotelAutomationApp.Application.Rooms.Commands;
using MediatR;

namespace HotelAutomationApp.Application.Rooms.UseCases
{
    public class CreateRoomUseCase : UseCase<CreateRoomRequest>
    {
        private readonly IMediator _mediator;

        public CreateRoomUseCase(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task HandleRequestAsync(CreateRoomRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateRoomCommand(
                request.MaxGuestsCount,
                request.Capacity,
                request.PricePerNight,
                request.RoomGroupId,
                request.Images);
            
            await _mediator.Send(command, cancellationToken);
        }
    }

    public class CreateRoomRequest : IRequest
    {
        public int MaxGuestsCount { get; set; }
        public double Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public string RoomGroupId { get; set; }
        public ICollection<ImageDto> Images { get; set; }
    }
}