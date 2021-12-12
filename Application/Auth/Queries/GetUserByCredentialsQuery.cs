using System;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Application.Auth.Models;
using HotelAutomationApp.Domain.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HotelAutomationApp.Application.Auth.Queries
{
    public class GetUserByCredentialsQuery : IRequest<User>
    {
        public GetUserByCredentialsQuery(UserCredentials userCredentials)
        {
            UserCredentials = userCredentials;
        }

        public UserCredentials UserCredentials { get; }
        
        private class Handler : IRequestHandler<GetUserByCredentialsQuery, User>
        {
            private readonly UserManager<User> _userManager;

            public Handler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<User> Handle(GetUserByCredentialsQuery request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.UserCredentials.Login);

                return user ?? throw new InvalidOperationException("User not found");
            }
        }
    }
}