using FluentValidation;
using HotelAutomationApp.Application.Auth.UseCases;
using HotelAutomationApp.Application.Extensions.FluentValidationExtensions;

namespace HotelAutomationApp.Application.Auth.Validators;

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(q => q.Email).IsValidEmail();
    }
}