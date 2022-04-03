using HotelAutomationApp.Application.Auth.Commands;
using HotelAutomationApp.Application.Auth.Models;
using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Exceptions;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HotelAutomationApp.Application.Auth.UseCases;

public class CreateUserUseCase : UseCase<CreateUserRequest>
{
    private readonly IMediator _mediator;
    private readonly ISecurityContext _securityContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateUserUseCase(
        IMediator mediator,
        ISecurityContext securityContext,
        UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _securityContext = securityContext;
        _userManager = userManager;
    }

    protected override async Task HandleRequestAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var contextUser = await _securityContext.GetCurrentUserAsync(CancellationToken.None);
        
        if (!contextUser!.CanLogin)
        {
            throw new PermissionDeniedException();
        }
        
        var userRoles = (await _userManager.GetRolesAsync(contextUser)).Select(Role.Get);
        
        var roles = request.Roles.Select(Role.Get);

        if (userRoles.Max()! <= roles.Max()!)
        {
            throw new PermissionDeniedException();
        }
        
        await _mediator.Send(new CreateUserCommand(
                request.Login,
                request.Password,
                request.Email,
                request.Roles),
            CancellationToken.None);
    }
}

public class CreateUserRequest : IRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string? Email { get; set; }
    public int[] Roles { get; set; }
}