using System.Threading;
using System.Threading.Tasks;
using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.Rooms.Commands;
using MediatR;

namespace HotelAutomationApp.Application.Rooms.UseCases
{
    public class DeleteRoomUseCase : UseCase<DeleteRoomRequest>
    {
        private readonly IMediator _mediator;

        public DeleteRoomUseCase(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task HandleRequestAsync(DeleteRoomRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteRoomCommand(request.RoomId), cancellationToken);
        }
    }

    public class DeleteRoomRequest : IRequest
    {
        public string RoomId { get; set; }
    }
}