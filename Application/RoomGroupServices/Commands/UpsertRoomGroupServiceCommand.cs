using AutoMapper;
using HotelAutomationApp.Application.Services.Models;
using HotelAutomationApp.Domain.Models.RoomGroupServices;
using HotelAutomationApp.Domain.Models.Services;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.RoomGroupServices.Commands;

public class UpsertRoomGroupServiceCommand : IRequest
{
    public UpsertRoomGroupServiceCommand(string roomGroupId, ICollection<ServiceDto> services)
    {
        RoomGroupId = roomGroupId;
        Services = services;
    }

    public string RoomGroupId { get; }
    public ICollection<ServiceDto> Services { get; }

    private class Handler : AsyncRequestHandler<UpsertRoomGroupServiceCommand>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDb, IMapper mapper)
        {
            _applicationDb = applicationDb;
            _mapper = mapper;
        }

        protected override async Task Handle(UpsertRoomGroupServiceCommand request, CancellationToken cancellationToken)
        {
            var roomGroup = await _applicationDb.RoomGroup.FindAsync(request.RoomGroupId); 

            var newServices = request.Services
                .ExcludeAfterFilter(q => !q.HasId, out var remain);

            var alreadyExistServices = await _applicationDb.Service
                .Where(q => remain.Select(w => w.Id)
                    .Any(w => w == q.Id))
                .ToListAsync(CancellationToken.None);

            newServices = remain
                .ExcludeSameElements(alreadyExistServices, first => first.Id, second => second.Id)
                .Concat(newServices).ToList();

            var mustBeAdded = alreadyExistServices.Select(q => new RoomGroupService(roomGroup!.Id, q.Id))
                .Concat(newServices.Select(q =>
                {
                    var @new = q with {Id = Guid.NewGuid().ToString()};
                    return new RoomGroupService(roomGroup!.Id, roomGroup, @new.Id, _mapper.Map<Service>(@new));
                })).ToList();

            _applicationDb.RoomGroupService.AddRange(mustBeAdded);
            await _applicationDb.SaveChangesAsync(CancellationToken.None);
        }
    }
}