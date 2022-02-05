using FluentValidation;
using HotelAutomationApp.Application.Rooms.UseCases;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Rooms.Validations
{
    public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
    {
        public CreateRoomRequestValidator(IDbContext db)
        {
            RuleFor(q => q.RoomGroupId)
                .MustAsync(async (field, token) =>
                    !string.IsNullOrEmpty(field) &&
                    await db.RoomGroups.FindAsync(new object[]{field}, token) is not null)
                .WithMessage("Room group not found");
        }
    }
}