using HotelAutomationApp.Domain.Models.RoomGroupServices;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;

namespace HotelAutomationApp.Application.RoomGroupServices.Commands;

public class BindRoomGroupServiceCommand : IRequest
{
    public BindRoomGroupServiceCommand(string roomGroupId, ICollection<string> services)
    {
        RoomGroupId = roomGroupId;
        ServiceIds = services;
    }

    public string RoomGroupId { get; }
    public ICollection<string> ServiceIds { get; }

    private class Handler : AsyncRequestHandler<BindRoomGroupServiceCommand>
    {
        private readonly IApplicationDbContext _applicationDb;

        public Handler(IApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }

        protected override async Task Handle(BindRoomGroupServiceCommand request, CancellationToken cancellationToken)
        {
            var roomGroup =
                await _applicationDb.RoomGroup.FindAsync(new object[] {request.RoomGroupId}, cancellationToken);

            var mustBeAddedServices = (from serviceId in request.ServiceIds
                        join service in _applicationDb.Service on serviceId equals service.Id
                        join rgs in _applicationDb.RoomGroupService on service.Id equals rgs.ServiceId into rgsGp
                        from rgs in rgsGp.DefaultIfEmpty()
                        where rgs == null
                        select service.Id)
                .Select(q => new RoomGroupService(Guid.NewGuid().ToString(), roomGroup!.Id, q))
                .ToList();

            _applicationDb.RoomGroupService.AddRange(mustBeAddedServices);
            await _applicationDb.SaveChangesAsync(CancellationToken.None);
        }
    }
}