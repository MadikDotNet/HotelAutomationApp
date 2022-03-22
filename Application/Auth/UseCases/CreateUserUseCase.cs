using HotelAutomationApp.Application.Auth.Commands;
using HotelAutomationApp.Application.Common;
using MediatR;

namespace HotelAutomationApp.Application.Auth.UseCases;

public class CreateUserUseCase : UseCase<CreateUserRequest>
{
    private readonly IMediator _mediator;

    public CreateUserUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task HandleRequestAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CreateUserCommand(
                request.Login,
                request.Password,
                request.Email,
                request.Roles),
            CancellationToken.None);
    }
}

public class CreateUserRequest : IRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string? Email { get; set; }
    public int[] Roles { get; set; }
}