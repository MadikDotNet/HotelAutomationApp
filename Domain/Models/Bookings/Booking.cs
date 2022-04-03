using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Common.Abstractions.Audition;
using HotelAutomationApp.Domain.Models.BookingServices;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Domain.Models.Services;
using HotelAutomationApp.Domain.Models.ValueObjects;
using HotelAutomationApp.Shared.Common.Abstractions;

namespace HotelAutomationApp.Domain.Models.Bookings;

public class Booking : BaseEntity, IPeriod, IAuditable
{
    [ForeignKey(nameof(Client))]
    public string ClientId { get; set; }
    public ApplicationUser Client { get; set; }
    public BookingState BookingState { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public Price TotalPrice { get; set; }
    [ForeignKey(nameof(Room))]
    public string RoomId { get; set; }
    public Room Room { get; set; }
    public ICollection<BookingService> Services { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreationDate { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime LastModifiedDate { get; set; }
}