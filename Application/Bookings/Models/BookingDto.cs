using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Services.Models;
using HotelAutomationApp.Domain.Models.Bookings;

namespace HotelAutomationApp.Application.Bookings.Models;

public record BookingDto : BaseEntityDto
{
    public string ClientId { get; set; }
    public string ClientName { get; set; }
    public string ClientEmail { get; set; }
    public string RoomId { get; set; }
    public BookingState BookingState { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public decimal TotalPrice { get; set; }
    public ServiceDto[] Services { get; set; }
}