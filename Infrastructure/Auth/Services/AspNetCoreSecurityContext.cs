using System;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Constants;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Exceptions;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Infrastructure.Auth.Services
{
    public class AspNetCoreSecurityContext : ISecurityContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDbContext _db;

        public AspNetCoreSecurityContext(
            IHttpContextAccessor httpContextAccessor,
            IDbContext db)
        {
            _httpContextAccessor = httpContextAccessor;
            _db = db;
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

        public async Task<User?> GetCurrentUserAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Users.FirstOrDefaultAsync(q => q.Id == UserId, cancellationToken);
        }
    }
}