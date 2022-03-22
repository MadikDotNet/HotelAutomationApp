using HotelAutomationApp.Application.Auth.Models;
using HotelAutomationApp.Domain.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HotelAutomationApp.Application.Auth.Commands;

public class CreateGuestUserCommand : IRequest
{
    public CreateGuestUserCommand(string login)
    {
        Login = login;
    }

    public string Login { get; }

    private class Handler : AsyncRequestHandler<CreateGuestUserCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public Handler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task Handle(CreateGuestUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = ApplicationUser.NewGuest(request.Login);

            var result = await _userManager.CreateAsync(user, ApplicationUser.DefaultGuestPassword);

            await _userManager.AddToRoleAsync(user, Role.Guest.ToString());

            if (!result.Succeeded)
            {
                throw new ArgumentException(string.Join(" ", result.Errors.Select(q => q.Description)));
            }
        }
    }
}