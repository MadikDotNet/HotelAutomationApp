using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.RoomGroups;
using HotelAutomationApp.Domain.Models.Services;

namespace HotelAutomationApp.Domain.Models.RoomGroupServices;

/// <summary>
/// Available services for certain room group
/// </summary>
public class RoomGroupService : BaseEntity
{
    public RoomGroupService()
    {
    }

    public RoomGroupService(string id, string roomGroupId, string serviceId)
    {
        Id = id;
        RoomGroupId = roomGroupId;
        ServiceId = serviceId;
    }

    public RoomGroupService(string id, string roomGroupId, RoomGroup roomGroup, string serviceId, Service service)
    {
        Id = id;
        RoomGroupId = roomGroupId;
        RoomGroup = roomGroup;
        ServiceId = serviceId;
        Service = service;
    }

    [ForeignKey(nameof(RoomGroup))] public string RoomGroupId { get; set; }
    public RoomGroup RoomGroup { get; set; }
    [ForeignKey(nameof(Service))] public string ServiceId { get; set; }
    public Service Service { get; set; }
}