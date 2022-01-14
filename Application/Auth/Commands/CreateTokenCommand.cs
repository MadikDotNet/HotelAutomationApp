using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using MediatR;

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

            public Handler(ITokenFactory tokenFactory)
            {
                _tokenFactory = tokenFactory;
            }

            public async Task<string> Handle(CreateTokenCommand command, CancellationToken cancellationToken)
            {
                return _tokenFactory.CreateToken(command.User);
            }
        }
    }
}