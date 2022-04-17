using FluentValidation;
using HotelAutomationApp.Application.Bookings.UseCases;
using HotelAutomationApp.Application.Rooms.Queries;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Bookings.Validators;

public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
{
    public CreateBookingRequestValidator(
        IApplicationDbContext applicationDb,
        ISecurityContext securityContext,
        IMediator mediator)
    {
        RuleFor(_ => _)
            .MustAsync(async (_, token) =>
                securityContext.UserExists && await applicationDb.User.AnyAsync(q => q.Id == securityContext.UserId,
                    token))
            .WithMessage("Client not found");

        RuleFor(q => q.Roomid)
            .MustAsync(async (roomId, token) => await applicationDb.Room.AnyAsync(q => q.Id == roomId, token))
            .WithMessage("Room not found");

        RuleFor(q => q)
            .MustAsync(async (request, token) =>
                (await mediator.Send(new GetAvailableRoomsByPeriodQuery(request, request.Roomid), token)).Any())
            .WithMessage("Room is not available for this time/period");

        When(request => request.ServiceIds is { } serviceIds && serviceIds.Any(), () =>
        {
            RuleFor(request => request.ServiceIds)
                .MustAsync(async (serviceIds, token) =>
                    await applicationDb.Service.Where(service => serviceIds.Contains(service.Id)).ToListAsync(token) is
                        { } list &&
                    list.Count == serviceIds.Length)
                .WithMessage("Some services not found");
        });
    }
}