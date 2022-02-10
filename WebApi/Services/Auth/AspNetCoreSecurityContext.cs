using System;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Constants;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Exceptions;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using HotelAutomationApp.Persistence.Interfaces.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.WebApi.Services.Auth
{
    public class AspNetCoreSecurityContext : ISecurityContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationDbContext _applicationDb;

        public AspNetCoreSecurityContext(
            IHttpContextAccessor httpContextAccessor,
            IApplicationDbContext applicationDb)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationDb = applicationDb;
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

        public async Task<ApplicationUser?> GetCurrentUserAsync(CancellationToken cancellationToken = default)
        {
            return await _applicationDb.User.FirstOrDefaultAsync(q => q.Id == UserId, cancellationToken);
        }
    }
}