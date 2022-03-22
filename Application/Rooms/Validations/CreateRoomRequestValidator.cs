using FluentValidation;
using HotelAutomationApp.Application.Rooms.UseCases;
using HotelAutomationApp.Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Rooms.Validations
{
    public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
    {
        public CreateRoomRequestValidator(IApplicationDbContext applicationDb)
        {
            RuleFor(q => q.RoomGroupId)
                .MustAsync(async (field, token) =>
                    !string.IsNullOrEmpty(field) &&
                    await applicationDb.RoomGroup.FindAsync(new object[]{field}, token) is not null)
                .WithMessage("Room group not found");

            RuleForEach(q => q.Files)
                .Must(file => file.Id is not null || file.File is not null)
                .WithMessage("File has invalid format");
        }
    }
}