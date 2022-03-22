using AutoMapper;
using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Rooms.Models;
using HotelAutomationApp.Application.Services.Models;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Rooms.UseCases;

public class ViewRoomByIdWithIncludedServicesUseCase : 
    UseCase<ViewRoomByIdWithIncludedServicesRequest, ViewRoomByIdWithAvailableServicesResponse>
{
    private readonly IApplicationDbContext _applicationDb;
    private readonly IMapper _mapper;
    
    public ViewRoomByIdWithIncludedServicesUseCase(IApplicationDbContext applicationDb, IMapper mapper)
    {
        _applicationDb = applicationDb;
        _mapper = mapper;
    }

    protected override async Task<ViewRoomByIdWithAvailableServicesResponse> HandleAsync(
        ViewRoomByIdWithIncludedServicesRequest request,
        CancellationToken cancellationToken)
    {
        var room = await _applicationDb.Room.Include(q => q.RoomGroup)
            .ThenInclude(q => q.RoomGroupServices)
            .FirstAsync(q => q.Id == request.RoomId, cancellationToken);

        return new ViewRoomByIdWithAvailableServicesResponse
        {
            Room = _mapper.Map<RoomDto>(room),
            IncudedServices = room.RoomGroup.RoomGroupServices
                .Select(service => _mapper.Map<ServiceDto>(service)).ToArray()
        };
    }
}

public class ViewRoomByIdWithIncludedServicesRequest : IRequest<ViewRoomByIdWithAvailableServicesResponse>
{
    public string RoomId { get; set; }
}

public class ViewRoomByIdWithAvailableServicesResponse
{
    public RoomDto Room { get; set; }
    public ServiceDto[] IncudedServices { get; set; }
}