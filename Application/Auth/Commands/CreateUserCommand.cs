using HotelAutomationApp.Application.Auth.Models;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HotelAutomationApp.Application.Auth.Commands
{
    public class CreateUserCommand : IRequest<string>
    {
        public CreateUserCommand(string login, string password, string? email, int[] roleIds)
        {
            Login = login.EnsureIsNotEmpty(nameof(Login));
            Password = password.EnsureIsNotEmpty(nameof(Password));
            Email = email;
            Roles = roleIds.Select(Role.Get).ToArray();
        }

        public string? Login { get; }
        public string? Password { get; }
        public string? Email { get; set; }
        public Role[] Roles { get; }

        private class Handler : IRequestHandler<CreateUserCommand, string>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = ApplicationUser.New(request.Login);
                user.Email = request.Email;

                var result = await _userManager.CreateAsync(user, request.Password);

                await _userManager.AddToRolesAsync(user, request.Roles.Select(q => q.Name));

                if (!result.Succeeded)
                {
                    throw new ArgumentException(string.Join(" ", result.Errors.Select(q => q.Description)));
                }

                return user.Id;
            }
        }
    }
}