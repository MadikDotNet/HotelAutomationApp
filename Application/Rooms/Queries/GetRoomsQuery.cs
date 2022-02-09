using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Rooms.Models;
using HotelAutomationApp.Domain.Models.Rooms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Rooms.Queries
{
    public class GetRoomsQuery : IRequest<ICollection<RoomDto>>
    {
        public GetRoomsQuery(
            Distance<int>? maxGuestsCountDistance,
            Distance<double>? capacityDistance,
            Distance<decimal>? priceDistance,
            bool isAvailable,
            bool isDeleted,
            string? roomGroupId)
        {
            MaxGuestsCountDistance = maxGuestsCountDistance;
            CapacityDistance = capacityDistance;
            PriceDistance = priceDistance;
            IsAvailable = isAvailable;
            RoomGroupId = roomGroupId;
        }

        public Distance<int>? MaxGuestsCountDistance { get; }
        public Distance<double>? CapacityDistance { get; }
        public Distance<decimal>? PriceDistance { get; }
        public bool IsAvailable { get; }
        public bool IsDeleted { get; }
        public string? RoomGroupId { get; }

        private class Handler : IRequestHandler<GetRoomsQuery, ICollection<RoomDto>>
        {
            private readonly IApplicationDbContext _applicationDb;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext applicationDb, IMapper mapper)
            {
                _applicationDb = applicationDb;
                _mapper = mapper;
            }

            public async Task<ICollection<RoomDto>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
            {
                IQueryable<Room> rooms = _applicationDb.Room
                    .AsNoTracking()
                    .Where(q => q.IsAvailable == request.IsAvailable && q.IsDeleted == request.IsDeleted)
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
                                             q.PricePerNight.Value >= request.PriceDistance.From)
                        .Where(q => request.PriceDistance.To == default ||
                                    q.PricePerNight.Value <= request.PriceDistance.To);
                }
                
                if (request.CapacityDistance is not null)
                {
                    rooms = rooms.Where(q => request.CapacityDistance.From == default ||
                                             q.Capacity >= request.CapacityDistance.From)
                        .Where(q => request.CapacityDistance.To == default ||
                                    q.Capacity <= request.CapacityDistance.To);
                }

                return await rooms.ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}