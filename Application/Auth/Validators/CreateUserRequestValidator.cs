using FluentValidation;
using HotelAutomationApp.Application.Auth.Models;
using HotelAutomationApp.Application.Auth.UseCases;
using HotelAutomationApp.Application.Extensions.FluentValidationExtensions;

namespace HotelAutomationApp.Application.Auth.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public bool IsRoleValid;

    public CreateUserRequestValidator()
    {
        RuleFor(q => q.Roles)
            .Must(q => q.Any())
            .Must(roles =>
            {
                IsRoleValid = roles.All(role => Role.TryGet(role, out _));

                return IsRoleValid;
            })
            .WithMessage("Role is invalid");

        When(_ => IsRoleValid,
            () =>
            {
                When(request => !string.IsNullOrWhiteSpace(request.Email),
                    () => { RuleFor(q => q.Email)!.IsValidEmail(); });
            });
    }
}