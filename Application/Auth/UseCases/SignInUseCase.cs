using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomation.Application.Auth.Commands;
using HotelAutomation.Application.Auth.Models;
using HotelAutomation.Application.Auth.Queries;
using HotelAutomation.Application.Common;
using HotelAutomation.Domain.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HotelAutomation.Application.Auth.UseCases
{
    public class SignInUseCase : UseCase<SignInRequest, SignInResponse>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public SignInUseCase(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        protected override async Task<SignInResponse> HandleAsync(
            SignInRequest request,
            CancellationToken cancellationToken)
        {
            var token = await _mediator.Send(new CreateTokenCommand(request.UserCredentials), cancellationToken);

            var user = await _mediator.Send(new GetUserByCredentialsQuery(request.UserCredentials), cancellationToken);

            var roles = await _userManager.GetRolesAsync(user);
            
            return new SignInResponse
            {
                UserId = user.Id, 
                AccessToken = token,
                Roles = roles,
                Username = user.UserName,
            };
        }
    }

    public class SignInRequest : IRequest<SignInResponse>
    {
        public UserCredentials UserCredentials { get; set; }
    }

    public class SignInResponse
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string Username { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}