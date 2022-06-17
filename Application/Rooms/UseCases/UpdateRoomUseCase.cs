using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.RoomMediaFiles.Commands;
using HotelAutomationApp.Application.Rooms.Commands;
using HotelAutomationApp.Application.Rooms.Models;
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
                request.PricePerHour,
                request.IsAvailable,
                request.RoomGroupId,
                request.Name,
                request.Description);

            await _mediator.Send(updateRoomCommand, CancellationToken.None);

            if (request.Files is not null && request.Files.Any())
            {
                await _mediator.Send(new UpsertRoomFilesCommand(request.Id, request.Files), CancellationToken.None);
            }
        }
    }

    public class UpdateRoomRequest : IRequest
    {
        public string Id { get; set; }
        public int MaxGuestsCount { get; set; }
        public double Capacity { get; set; }
        public decimal PricePerHour { get; set; }
        public bool IsAvailable { get; set; }
        public string RoomGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<FileDto>? Files { get; set; }
    }
}