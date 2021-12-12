using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Application.Auth.Models;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Exceptions;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Auth.Commands
{
    public class CreateTokenCommand : IRequest<string>
    {
        public UserCredentials UserCredentials { get; set; }

        public CreateTokenCommand(UserCredentials userCredentials)
        {
            UserCredentials = userCredentials;
        }

        private class Handler : IRequestHandler<CreateTokenCommand, string>
        {
            private readonly UserManager<User> _userManager;
            private readonly ITokenFactory _tokenFactory;

            public Handler(
                UserManager<User> userManager,
                ITokenFactory tokenFactory)
            {
                _userManager = userManager;
                _tokenFactory = tokenFactory;
            }

            public async Task<string> Handle(CreateTokenCommand command, CancellationToken cancellationToken)
            {
                var credentials = command.UserCredentials;

                var user = await _userManager.Users.FirstOrDefaultAsync(
                    q => q.UserName == credentials.Login, cancellationToken);

                if (user is null || !await _userManager.CheckPasswordAsync(user, credentials.Password))
                {
                    throw new InvalidCredentialsException();
                }

                return _tokenFactory.CreateToken(user);
            }
        }
    }
}