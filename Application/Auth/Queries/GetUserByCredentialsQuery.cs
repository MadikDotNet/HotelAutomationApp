using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Auth.Queries
{
    public class GetUserByCredentialsQuery : IRequest<ApplicationUser>
    {
        public GetUserByCredentialsQuery(string login, string password)
        {
            Login = login;
            Password = password;
        }
        
        public string Login { get; }
        public string Password { get; }

        private class Handler : IRequestHandler<GetUserByCredentialsQuery, ApplicationUser>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<ApplicationUser> Handle(GetUserByCredentialsQuery request,
                CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.Login);

                return user ?? throw new InvalidOperationException("User not found");
            }
        }
    }
}