using FluentValidation;
using HotelAutomationApp.Application.Rooms.UseCases;
using HotelAutomationApp.Domain.Models.Rooms;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Rooms.Validations
{
    public class UpdateRoomRequestValidator : AbstractValidator<UpdateRoomRequest>
    {
        private static Room Object = null;
        
        public UpdateRoomRequestValidator(IDbContext db)
        {
            RuleFor(q => q.RoomDto.Id)
                .MustAsync(async (field, token) =>
                {
                    Object = await db.Rooms.FindAsync(new object[]{field}, token);
                    return true;
                });

            RuleFor(q => q.RoomDto)
                .Must(field => Object is null)
                .WithMessage("Room not found");

            RuleFor(q => q.RoomDto)
                .Must(field => field.RoomGroupId is not null)
                .WithMessage("Room group not found");
        }
    }
}