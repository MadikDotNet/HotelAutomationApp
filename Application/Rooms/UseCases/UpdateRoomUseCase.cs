using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.MediaFiles.Models;
using HotelAutomationApp.Application.RoomMedia.Commands;
using HotelAutomationApp.Application.Rooms.Commands;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;

namespace HotelAutomationApp.Application.Rooms.UseCases
{
    public class UpdateRoomUseCase : TransactionUseCase<UpdateRoomRequest>
    {
        private readonly IMediator _mediator;

        public UpdateRoomUseCase(IMediator mediator, IApplicationDbContext applicationDb) : base(applicationDb)
        {
            _mediator = mediator;
        }

        protected override async Task HandleRequestAsync(UpdateRoomRequest request, CancellationToken cancellationToken)
        {
            var updateRoomCommand = new UpdateRoomCommand(
                request.Id,
                request.MaxGuestsCount,
                request.Capacity,
                request.PricePerNight,
                request.IsAvailable,
                request.RoomGroupId);
            
            await _mediator.Send(updateRoomCommand, CancellationToken.None);

            throw new Exception();
            await _mediator.Send(new UpsertRoomMediaCommand(request.Id, request.Media), CancellationToken.None);
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
        public ICollection<MediaDto> Media { get; set; }
    }
}