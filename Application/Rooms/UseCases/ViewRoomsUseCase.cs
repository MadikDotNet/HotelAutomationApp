using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Rooms.Models;
using HotelAutomationApp.Application.Rooms.Queries;
using MediatR;

namespace HotelAutomationApp.Application.Rooms.UseCases
{
    public class ViewRoomsUseCase : UseCase<ViewRoomsRequest, ICollection<RoomDto>>
    {
        private readonly IMediator _mediator;

        public ViewRoomsUseCase(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task<ICollection<RoomDto>> HandleAsync(ViewRoomsRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetRoomsQuery(
                request.MaxGuestsCountDistance,
                request.CapacityDistance,
                request.PriceDistance,
                request.IsAvailable,
                request.IsDeleted,
                request.RoomGroupId), cancellationToken);
        }
    }

    public class ViewRoomsRequest : IRequest<ICollection<RoomDto>>
    {
        public Distance<int>? MaxGuestsCountDistance { get; set; }
        public Distance<double>? CapacityDistance { get; set; }
        public Distance<decimal>? PriceDistance { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public string? RoomGroupId { get; set; }
    }
}