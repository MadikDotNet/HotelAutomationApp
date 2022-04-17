using FluentValidation;
using HotelAutomationApp.Application.Appeals.UseCases;
using HotelAutomationApp.Application.Extensions.FluentValidationExtensions;

namespace HotelAutomationApp.Application.Appeals.Validators;

public class CreateAppealRequestValidator : AbstractValidator<CreateAppealRequest>
{
    public CreateAppealRequestValidator()
    {
        RuleFor(q => q.Email)
            .IsValidEmail();

        RuleFor(q => q.Body)
            .NotEmpty();

        RuleFor(q => q.Title)
            .NotEmpty();
    }
}