using HotelAutomationApp.Shared.Extensions;
using Microsoft.AspNetCore.Identity;

namespace HotelAutomationApp.Domain.Models.Identity
{
    public sealed class ApplicationUser : IdentityUser
    {
        public const string DefaultGuestPassword = "DefaultGuestPassword";

        public ApplicationUser()
        {
        }

        public ApplicationUser(
            string username,
            string phoneNumber,
            string email,
            DateTime createdDate,
            DateTime? deletedDate,
            DateTime? blockedDate) : base(username)
        {
            PhoneNumber = phoneNumber;
            Email = email;
            CreatedDate = createdDate;
            DeletedDate = deletedDate;
            BlockedDate = blockedDate;
        }

        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? BlockedDate { get; set; }
        public bool IsGuest { get; set; }
        public bool CanLogin => DeletedDate is null && BlockedDate is null;

        public static ApplicationUser New(string? userName)
        {
            userName.EnsureIsNotEmpty(nameof(userName));

            return new ApplicationUser
            {
                UserName = userName,
                CreatedDate = DateTime.UtcNow,
                IsGuest = false
            };
        }

        public static ApplicationUser NewGuest(string userName)
        {
            userName.EnsureIsNotEmpty(nameof(userName));

            return new ApplicationUser
            {
                UserName = userName,
                CreatedDate = DateTime.UtcNow,
                IsGuest = true
            };
        }
    }
}