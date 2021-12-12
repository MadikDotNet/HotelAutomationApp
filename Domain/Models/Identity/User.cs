using System;
using HotelAutomationApp.Shared;
using Microsoft.AspNetCore.Identity;

namespace HotelAutomationApp.Domain.Models.Identity
{
    public sealed class User : IdentityUser
    {
        public User()
        {
        }

        public User(
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
        public bool CanLogin => DeletedDate is null && BlockedDate is null;

        public static User New(string userName)
        {
            userName.ThrowIfArgNullOrWhiteSpace(nameof(userName));

            return new User
            {
                UserName = userName,
                CreatedDate = DateTime.UtcNow
            };
        }
    }
}