using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Domain.Models.Rooms;

namespace HotelAutomationApp.Domain.Models.Orders;

public class Booking : BaseEntity
{
    [ForeignKey(nameof(Client))]
    public string ClientId { get; set; }
    public ApplicationUser Client { get; set; }
    public BookingState BookingState { get; set; }
    [ForeignKey(nameof(Room))]
    public string RoomId { get; set; }
    public Room Room { get; set; }
}