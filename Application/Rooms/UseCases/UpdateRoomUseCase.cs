using System.Threading;
using System.Threading.Tasks;
using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.Rooms.Commands;
using HotelAutomationApp.Application.Rooms.Models;
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
            await _mediator.Send(new UpdateRoomCommand(request.RoomDto), cancellationToken);
        }
    }

    public class UpdateRoomRequest : IRequest
    {
        public RoomDto RoomDto { get; set; }
    }
}