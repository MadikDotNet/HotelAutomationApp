using FluentValidation;
using HotelAutomationApp.Application.MediaFiles.UseCases;
using HotelAutomationApp.Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.MediaFiles.Validators;

public class DeleteMediaRequestValidator : AbstractValidator<DeleteMediaRequest>
{
    public DeleteMediaRequestValidator(IApplicationDbContext applicationDb)
    {
        RuleFor(q => q.FileId)
            .MustAsync(async (field, token) => await applicationDb.FileMetadata.FindAsync(field, token) is { })
            .WithMessage("FileMetadata file not found");
    }
}