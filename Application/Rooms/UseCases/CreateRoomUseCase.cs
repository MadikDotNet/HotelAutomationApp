using System.Threading;
using System.Threading.Tasks;
using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.Rooms.Commands;
using HotelAutomationApp.Application.Rooms.Models;
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
            await _mediator.Send(new CreateRoomCommand(request.RoomDto), cancellationToken);
        }
    }

    public class CreateRoomRequest : IRequest
    {
        public RoomDto RoomDto { get; set; }
    }
}