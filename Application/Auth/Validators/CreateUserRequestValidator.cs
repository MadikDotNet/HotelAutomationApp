using FluentValidation;
using HotelAutomationApp.Application.Auth.Models;
using HotelAutomationApp.Application.Auth.UseCases;
using HotelAutomationApp.Application.Extensions.FluentValidationExtensions;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using Microsoft.AspNetCore.Identity;

namespace HotelAutomationApp.Application.Auth.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public bool IsRoleValid;

    public CreateUserRequestValidator(ISecurityContext securityContext, UserManager<ApplicationUser> userManager)
    {
        RuleFor(q => q.Roles)
            .Must(roles =>
            {
                IsRoleValid = roles.All(role => Role.TryGet(role, out _));

                return IsRoleValid;
            })
            .WithMessage("Role is invalid");

        When(request => IsRoleValid, () =>
        {
            When(request => !string.IsNullOrWhiteSpace(request.Email), () =>
            {
                RuleFor(q => q.Email)!.IsValidEmail();
            });

            RuleFor(q => q.Roles)
                .MustAsync(async (roleIds, token) =>
                {
                    if (await securityContext.GetCurrentUserAsync(CancellationToken.None) is not { } user ||
                        !user.CanLogin)
                    {
                        return false;
                    }

                    var currentUserRoles = (await userManager.GetRolesAsync(user)).Select(Role.Get);

                    var roles = roleIds.Select(Role.Get);

                    return currentUserRoles.Max() > roles.Max();
                })
                .WithMessage("Permission denied");
        });
    }
}