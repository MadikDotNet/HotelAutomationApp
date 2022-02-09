using FluentValidation;
using HotelAutomationApp.Application.Rooms.UseCases;
using HotelAutomationApp.Domain.Models.Rooms;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Rooms.Validations
{
    public class UpdateRoomRequestValidator : AbstractValidator<UpdateRoomRequest>
    {
        private static Room? _object;
        
        public UpdateRoomRequestValidator(IApplicationDbContext applicationDb)
        {
            RuleFor(q => q.Id)
                .MustAsync(async (field, token) =>
                {
                    _object = await applicationDb.Room.FindAsync(new object[]{field}, token);
                    return true;
                });

            RuleFor(q => q.Id)
                .Must(field => !(_object is null))
                .WithMessage("Room not found");

            RuleFor(q => q.RoomGroupId)
                .Must(field => field is not null)
                .WithMessage("Room group not found");
        }
    }
}