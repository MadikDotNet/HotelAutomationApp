using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.Bookings;
using HotelAutomationApp.Domain.Models.Services;

namespace HotelAutomationApp.Domain.Models.BookingServices;

public class BookingService : BaseEntity
{
    [ForeignKey(nameof(Booking))]
    public string BookingId { get; set; }
    public Booking Booking { get; set; }
    [ForeignKey(nameof(Service))]
    public string ServiceId { get; set; }
    public Service Service { get; set; }
}