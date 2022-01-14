using FluentValidation;
using HotelAutomationApp.Application.Rooms.UseCases;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Rooms.Validations
{
    public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
    {
        public CreateRoomRequestValidator(IDbContext db)
        {
            RuleFor(q => q.RoomDto)
                .MustAsync(async (field, token) =>
                    field.RoomGroupId is not null ||
                    await db.RoomGroups.FindAsync(field.RoomGroupId, token) is not null)
                .WithMessage("Room group not found");
        }
    }
}