using System.Threading;
using System.Threading.Tasks;
using HotelAutomation.Application.Auth.Commands;
using HotelAutomation.Application.Common;
using MediatR;

namespace HotelAutomation.Application.Auth.UseCases
{
    public class SignUpUseCase : UseCase<SignUpRequest>
    {
        private readonly IMediator _mediator;

        public SignUpUseCase(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task HandleRequestAsync(SignUpRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreateUserCommand(request.Login, request.Password, request.Role), cancellationToken);
        }
    }

    public class SignUpRequest : IRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}