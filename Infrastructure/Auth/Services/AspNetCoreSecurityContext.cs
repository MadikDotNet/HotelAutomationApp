using System;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomation.Domain.Models.Identity;
using HotelAutomation.Infrastructure.Auth.Constants;
using HotelAutomation.Infrastructure.Auth.Exceptions;
using HotelAutomation.Infrastructure.Auth.Services;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Constants;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Exceptions;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Auth.Services
{
    public class AspNetCoreSecurityContext : ISecurityContext
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AspNetCoreSecurityContext(
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool UserExists {
            get
            {
                return _httpContextAccessor.HttpContext?.User?.Identity is not null;
            }
        }
        public string UserId {
            get
            {
                if (!UserExists)
                {
                    throw new NotAuthenticatedException();
                }

                try
                {
                    return _httpContextAccessor.HttpContext.User.FindFirst(Claims.Subject).Value;
                }
                catch (Exception e)
                {
                    throw new NotAuthenticatedException();
                }
            }
        }
        public async Task<User?> GetUserAsync(CancellationToken cancellationToken = default)
        {
            return await _userManager.Users.FirstOrDefaultAsync(q => q.Id == UserId, cancellationToken);
        }
    }
}