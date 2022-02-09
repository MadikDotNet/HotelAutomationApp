using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Shared;
using HotelAutomationApp.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HotelAutomationApp.Application.Auth.Commands
{
    public class CreateUserCommand : IRequest
    {
        public CreateUserCommand(string login, string password, string role)
        {
            Login = login.EnsureIsNotEmpty(nameof(Login));
            Password = password.EnsureIsNotEmpty(nameof(Password));
            Role = role.EnsureIsNotEmpty(nameof(Role));
        }

        public string Login { get; }
        public string Password { get; }
        public string Role { get; }

        private class Handler : AsyncRequestHandler<CreateUserCommand>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            protected override async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = ApplicationUser.New(request.Login);

                var result = await _userManager.CreateAsync(user, request.Password);
                // await _userManager.AddToRoleAsync(user, request.Role);

                if (!result.Succeeded)
                {
                    throw new ArgumentException(string.Join(" ", result.Errors.Select(q => q.Description)));
                }
            }
        }
    }
}