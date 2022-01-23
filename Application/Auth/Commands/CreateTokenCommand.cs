using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Constants;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HotelAutomationApp.Application.Auth.Commands
{
    public class CreateTokenCommand : IRequest<string>
    {
        public User User { get; set; }

        public CreateTokenCommand(User user)
        {
            User = user;
        }

        private class Handler : IRequestHandler<CreateTokenCommand, string>
        {
            private readonly ITokenFactory _tokenFactory;
            private readonly UserManager<User> _userManager;

            public Handler(ITokenFactory tokenFactory, UserManager<User> userManager)
            {
                _tokenFactory = tokenFactory;
                _userManager = userManager;
            }

            public async Task<string> Handle(CreateTokenCommand command, CancellationToken cancellationToken)
            {
                var claims = new Dictionary<string, object>
                {
                    [Claims.Subject] = command.User.Id
                };
                
                var userRoles = (await _userManager.GetRolesAsync(command.User)).ToList();

                userRoles.ForEach(role => claims[Claims.Role] = role);

                return _tokenFactory.CreateToken(claims);
            }
        }
    }
}