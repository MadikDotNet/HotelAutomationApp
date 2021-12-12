using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomation.Domain.Models.Identity;
using HotelAutomation.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HotelAutomation.Application.Auth.Commands
{
    public class CreateUserCommand : IRequest
    {
        public CreateUserCommand(string login, string password, string role)
        {
            Login = login.ThrowIfArgNullOrWhiteSpace(nameof(Login));
            Password = password.ThrowIfArgNullOrWhiteSpace(nameof(Password));
            Role = role.ThrowIfArgNullOrWhiteSpace(nameof(Role));
        }

        public string Login { get; }
        public string Password { get; }
        public string Role { get; }

        private class Handler : AsyncRequestHandler<CreateUserCommand>
        {
            private readonly UserManager<User> _userManager;

            public Handler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            protected override async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = User.New(request.Login);

                var result = await _userManager.CreateAsync(user, request.Password);
                await _userManager.AddToRoleAsync(user, request.Role);

                if (!result.Succeeded)
                {
                    throw new ArgumentException(string.Join(" ", result.Errors.Select(q => q.Description)));
                }
            }
        }
    }
}