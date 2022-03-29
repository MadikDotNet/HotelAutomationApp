using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Rooms.Models;
using HotelAutomationApp.Application.Rooms.Queries;
using MediatR;

namespace HotelAutomationApp.Application.Rooms.UseCases
{
    public class ViewRoomsUseCase : UseCase<ViewRoomsRequest, PageResponse<RoomDto>>
    {
        private readonly IMediator _mediator;

        public ViewRoomsUseCase(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task<PageResponse<RoomDto>> HandleAsync(
            ViewRoomsRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetRoomsQuery(
                request.PageRequest,
                request.MaxGuestsCountDistance,
                request.CapacityDistance,
                request.PriceDistance,
                request.IsAvailable,
                request.IsDeleted,
                request.RoomGroupId,
                request.Name,
                request.Description,
                request.FullMatching), cancellationToken);
        }
    }

    public class ViewRoomsRequest : IRequest<PageResponse<RoomDto>>
    {
        public PageRequest? PageRequest { get; set; }
        public Distance<int>? MaxGuestsCountDistance { get; set; }
        public Distance<double>? CapacityDistance { get; set; }
        public Distance<decimal>? PriceDistance { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool FullMatching { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public string? RoomGroupId { get; set; }
    }
}