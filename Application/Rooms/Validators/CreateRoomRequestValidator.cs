using FluentValidation;
using HotelAutomationApp.Application.Rooms.UseCases;
using HotelAutomationApp.Domain.Models.ValueObjects;
using HotelAutomationApp.Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Rooms.Validators
{
    public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
    {
        private Price? _minPrice;

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
                        _minPrice = roomGroup?.MinPrice;
                        return roomGroup is not null;
                    }
                )
                .WithMessage("Room group not found");

            When(_ => _minPrice is { }, () =>
            {
                RuleFor(q => q.PricePerHour)
                    .Must(price => price <= _minPrice!)
                    .WithMessage("Price cannot be less than room's group min price declaration");

                RuleForEach(q => q.Files)
                    .Must(file => file.Id is not null ||
                                  file.FileType is not null &&
                                  file.FileName is not null &&
                                  file.Content is not null)
                    .WithMessage("File has invalid format");
            });
        }
    }
}