using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Extensions;
using HotelAutomationApp.Application.Rooms.Models;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Persistence.Interfaces.Context;
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
            bool fullMatching)
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
        }

        public PageRequest? PageRequest { get; }
        public Distance<int>? MaxGuestsCountDistance { get; }
        public Distance<double>? CapacityDistance { get; }
        public Distance<decimal>? PriceDistance { get; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool FullMatching { get; set; }
        public bool IsAvailable { get; }
        public bool IsDeleted { get; }
        public string? RoomGroupId { get; }

        private class Handler : IRequestHandler<GetRoomsQuery, PageResponse<RoomDto>>
        {
            private readonly IApplicationDbContext _applicationDb;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext applicationDb, IMapper mapper)
            {
                _applicationDb = applicationDb;
                _mapper = mapper;
            }

            public async Task<PageResponse<RoomDto>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
            {
                IQueryable<Room> rooms = _applicationDb.Room
                    .AsNoTracking()
                    .Where(q => q.IsDeleted == request.IsDeleted)
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

                var roomDtos = await rooms.ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var now = DateTime.UtcNow;

                var bookings = _applicationDb.Booking.Where(q => q.DateFrom < now && q.DateTo > now);

                var result = (from room in roomDtos
                    join booking in bookings on room.Id equals booking.RoomId into bookingsGp
                    from booking in bookingsGp.DefaultIfEmpty()
                    select room with
                    {
                        IsAvailable = booking is null
                    }).ToList();

                return result.AsPageResponse(request.PageRequest);
            }
        }
    }
}