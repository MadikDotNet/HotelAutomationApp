using FluentValidation;
using HotelAutomationApp.Application.Rooms.UseCases;
using HotelAutomationApp.Domain.Models.Rooms;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Rooms.Validations
{
    public class DeleteRoomRequestValidator : AbstractValidator<DeleteRoomRequest>
    {
        private static Room Object;

        public DeleteRoomRequestValidator(IDbContext db)
        {
            RuleFor(q => q.RoomId)
                .MustAsync(async (field, token) =>
                {
                    Object = await db.Rooms.FindAsync(new object[]{field}, token);
                    return true;
                });

            RuleFor(q => q.RoomId)
                .Must(q => Object is not null)
                .WithMessage("Room not found");

            RuleFor(q => q.RoomId)
                .Must(q => !Object.IsDeleted)
                .WithMessage("Room already been deleted");
        }
    }
}