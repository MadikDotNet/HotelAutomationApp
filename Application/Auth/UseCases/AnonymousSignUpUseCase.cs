using HotelAutomationApp.Application.Auth.Commands;
using HotelAutomationApp.Application.Auth.Models;
using HotelAutomationApp.Application.Common;
using MediatR;

namespace HotelAutomationApp.Application.Auth.UseCases;

public class AnonymousSignUpUseCase : UseCase<AnonymousSignUpRequest, string>
{
    private readonly IMediator _mediator;

    public AnonymousSignUpUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<string> HandleAsync(AnonymousSignUpRequest request,
        CancellationToken cancellationToken)
    {
        var userId = await _mediator.Send(new CreateUserCommand(
            request.Login,
            request.Login,
            request.Email,
            new[] {Role.Guest.Key}), cancellationToken);

        return userId;
    }
}

public class AnonymousSignUpRequest : IRequest<string>
{
    public string Login { get; set; }
    public string Email { get; set; }
}