using HotelAutomationApp.Application.Auth.Commands;
using HotelAutomationApp.Application.Auth.Models;
using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Infrastructure.Interfaces.Security.Services;
using MediatR;

namespace HotelAutomationApp.Application.Auth.UseCases;

public class AnonymousSignUpUseCase : UseCase<AnonymousSignUpRequest, string>
{
    private readonly IMediator _mediator;
    private readonly IHashService _hashService;

    public AnonymousSignUpUseCase(IMediator mediator, IHashService hashService)
    {
        _mediator = mediator;
        _hashService = hashService;
    }

    protected override async Task<string> HandleAsync(AnonymousSignUpRequest request,
        CancellationToken cancellationToken)
    {
        var passwordHash = _hashService.GetHash(request.Email);

        var userId = await _mediator.Send(new CreateUserCommand(
            request.Login,
            passwordHash,
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