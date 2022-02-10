using FluentValidation;
using HotelAutomationApp.Application.Rooms.UseCases;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Rooms.Validations
{
    public class DeleteRoomRequestValidator : AbstractValidator<DeleteRoomRequest>
    {
        private static Room? _object;

        public DeleteRoomRequestValidator(IApplicationDbContext applicationDb)
        {
            RuleFor(q => q.RoomId)
                .MustAsync(async (field, token) =>
                {
                    _object = await applicationDb.Room.FindAsync(new object[]{field}, token);
                    return true;
                });

            RuleFor(q => q.RoomId)
                .Must(q => _object is not null)
                .WithMessage("Room not found");

            RuleFor(q => q.RoomId)
                .Must(q => _object!.IsDeleted)
                .WithMessage("Room already been deleted");
        }
    }
}