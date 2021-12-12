using System;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Constants;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Exceptions;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using Microsoft.AspNetCore.Http;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Infrastructure.Auth.Services
{
    public class AspNetCoreSecurityContext : ISecurityContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AspNetCoreSecurityContext(
            IHttpContextAccessor httpContextAccessor)
        {
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
    }
}