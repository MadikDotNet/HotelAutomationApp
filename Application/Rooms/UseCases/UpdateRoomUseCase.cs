using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.File.Models;
using HotelAutomationApp.Application.Rooms.Commands;
using MediatR;

namespace HotelAutomationApp.Application.Rooms.UseCases
{
    public class UpdateRoomUseCase : UseCase<UpdateRoomRequest>
    {
        private readonly IMediator _mediator;

        public UpdateRoomUseCase(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task HandleRequestAsync(UpdateRoomRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateRoomCommand(
                request.Id,
                request.MaxGuestsCount,
                request.Capacity,
                request.PricePerNight,
                request.IsAvailable,
                request.RoomGroupId,
                request.Images);
            
            await _mediator.Send(command, cancellationToken);
        }
    }

    public class UpdateRoomRequest : IRequest
    {
        public string Id { get; set; }
        public int MaxGuestsCount { get; set; }
        public double Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public string RoomGroupId { get; set; }
        public ICollection<MediaDto> Images { get; set; }
    }
}