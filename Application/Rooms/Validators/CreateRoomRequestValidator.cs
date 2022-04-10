using FluentValidation;
using HotelAutomationApp.Application.Rooms.UseCases;
using HotelAutomationApp.Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Rooms.Validators
{
    public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
    {
        public CreateRoomRequestValidator(IApplicationDbContext applicationDb)
        {
            RuleFor(q => q.RoomGroupId)
                .MustAsync(async (field, token) =>
                    {
                        if (string.IsNullOrEmpty(field))
                        {
                            return false;
                        }

                        var roomGroup = await applicationDb.RoomGroup.FindAsync(new object[] {field}, token);
                        return roomGroup is not null;
                    }
                )
                .WithMessage("Room group not found");

            RuleFor(q => new {q.PricePerHour, q.RoomGroupId})
                .MustAsync(async (fields, token) =>
                    await applicationDb.RoomGroup.FindAsync(new object[] {fields.RoomGroupId}, token) is { } roomGp &&
                    fields.PricePerHour >= roomGp.MinPrice)
                .WithMessage("Price cannot be less than room's group min price declaration");

            RuleForEach(q => q.Files)
                .Must(file => file.Id is not null ||
                              file.FileType is not null &&
                              file.FileName is not null &&
                              file.Content is not null)
                .WithMessage("File has invalid format");
        }
    }
}