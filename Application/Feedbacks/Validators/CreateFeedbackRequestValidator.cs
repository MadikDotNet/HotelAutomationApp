using FluentValidation;
using HotelAutomationApp.Application.Feedbacks.UseCases;
using HotelAutomationApp.Domain.Models.Messaging.Appeals;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Common;

namespace HotelAutomationApp.Application.Feedbacks.Validators;

public class CreateFeedbackRequestValidator : AbstractValidator<CreateFeedbackRequest>
{
    public CreateFeedbackRequestValidator(IApplicationDbContext applicationDb)
    {
        RuleFor(q => q.Body)
            .NotEmpty()
            .WithMessage("Body cannot be empty");

        RuleFor(q => q.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty");

        RuleFor(q => q.AppealId)
            .MustAsync(async (id, token) =>
                await applicationDb.Appeal.FindAsync(id.YieldObjectArray(), token)
                    is {Status: AppealStatus.Written})
            .WithMessage("Appeal not found or already was answered");
    }
}