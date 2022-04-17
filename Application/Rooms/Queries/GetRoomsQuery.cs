using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Extensions;
using HotelAutomationApp.Application.Rooms.Models;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Common.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HotelAutomationApp.Application.Rooms.Queries
{
    public class GetRoomsQuery : IRequest<PageResponse<RoomDto>>
    {
        public GetRoomsQuery(
            PageRequest? pageRequest,
            Distance<int>? maxGuestsCountDistance,
            Distance<double>? capacityDistance,
            Distance<decimal>? priceDistance,
            bool isAvailable,
            bool isDeleted,
            string? roomGroupId,
            string? name,
            string? description,
            bool fullMatching,
            IPeriod? period)
        {
            PageRequest = pageRequest;
            MaxGuestsCountDistance = maxGuestsCountDistance;
            CapacityDistance = capacityDistance;
            PriceDistance = priceDistance;
            IsAvailable = isAvailable;
            RoomGroupId = roomGroupId;
            Name = name;
            Description = description;
            FullMatching = fullMatching;
            IsDeleted = isDeleted;
            Period = period;
        }

        public PageRequest? PageRequest { get; }
        public Distance<int>? MaxGuestsCountDistance { get; }
        public Distance<double>? CapacityDistance { get; }
        public Distance<decimal>? PriceDistance { get; }
        public string? Name { get; }
        public string? Description { get; }
        public bool FullMatching { get; }
        public bool IsAvailable { get; }
        public IPeriod? Period { get; }
        public bool IsDeleted { get; }
        public string? RoomGroupId { get; }

        private class Handler : IRequestHandler<GetRoomsQuery, PageResponse<RoomDto>>
        {
            private readonly IMediator _mediator;
            private readonly IApplicationDbContext _applicationDb;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext applicationDb, IMapper mapper, IMediator mediator)
            {
                _applicationDb = applicationDb;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<PageResponse<RoomDto>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
            {
                var rooms = request.IsAvailable
                    ? (await _mediator.Send(new GetAvailableRoomsByPeriodQuery(request.Period), cancellationToken))
                    .AsQueryable()
                    : _applicationDb.Room.AsQueryable();

                rooms = rooms.Where(q => q.IsDeleted == request.IsDeleted)
                    .Where(q => string.IsNullOrEmpty(request.RoomGroupId) || q.RoomGroupId == request.RoomGroupId);

                if (request.MaxGuestsCountDistance is not null)
                {
                    rooms = rooms.Where(q => request.MaxGuestsCountDistance.From == default ||
                                             q.MaxGuestsCount >= request.MaxGuestsCountDistance.From)
                        .Where(q => request.MaxGuestsCountDistance.To == default ||
                                    q.MaxGuestsCount <= request.MaxGuestsCountDistance.To);
                }

                if (request.PriceDistance is not null)
                {
                    rooms = rooms.Where(q => request.PriceDistance.From == default ||
                                             q.PricePerHour >= request.PriceDistance.From)
                        .Where(q => request.PriceDistance.To == default ||
                                    q.PricePerHour <= request.PriceDistance.To);
                }

                if (request.CapacityDistance is not null)
                {
                    rooms = rooms.Where(q => request.CapacityDistance.From == default ||
                                             q.Capacity >= request.CapacityDistance.From)
                        .Where(q => request.CapacityDistance.To == default ||
                                    q.Capacity <= request.CapacityDistance.To);
                }

                if (!string.IsNullOrEmpty(request.Name))
                {
                    rooms = rooms.Where(q =>
                        request.FullMatching ? q.Name == request.Name : q.Name.Contains(request.Name));
                }

                if (!string.IsNullOrEmpty(request.Description))
                {
                    rooms = rooms.Where(q =>
                        request.FullMatching
                            ? q.Description == request.Description
                            : q.Description.Contains(request.Description));
                }

                var result = rooms.Select(room => _mapper.Map<RoomDto>(room)).ToList();

                return result
                    .Localize()
                    .AsPageResponse(request.PageRequest);
            }
        }
    }
}