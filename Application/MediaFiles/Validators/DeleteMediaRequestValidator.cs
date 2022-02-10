using FluentValidation;
using HotelAutomationApp.Application.MediaFiles.UseCases;
using HotelAutomationApp.Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.MediaFiles.Validators;

public class DeleteMediaRequestValidator : AbstractValidator<DeleteMediaRequest>
{
    public DeleteMediaRequestValidator(IApplicationDbContext applicationDb)
    {
        RuleFor(q => q.MediaId)
            .MustAsync(async (field, token) => await applicationDb.Media.FindAsync(field, token) is { } media)
            .WithMessage("Media file not found");
    }
}