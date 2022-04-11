using HotelAutomationApp.Application.Common;

namespace HotelAutomationApp.Application.Users.Models;

public record UserDto : BaseEntityDto
{
    public DateTime CreatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public DateTime? BlockedDate { get; set; }
    public bool IsGuest { get; set; }
    public  string Email { get; set; }
    public  string Name { get; set; }
    public virtual string UserName { get; set; }
}