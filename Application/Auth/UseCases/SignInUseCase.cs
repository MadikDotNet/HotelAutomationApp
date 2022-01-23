using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomation.Application.Common;
using HotelAutomationApp.Application.Auth.Commands;
using HotelAutomationApp.Application.Auth.Models;
using HotelAutomationApp.Application.Auth.Queries;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HotelAutomationApp.Application.Auth.UseCases
{
    public class SignInUseCase : UseCase<SignInRequest, SignInResponse>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;
        private readonly ITokenFactory _tokenFactory;

        public SignInUseCase(
            IMediator mediator,
            UserManager<User> userManager,
            ITokenFactory tokenFactory)
        {
            _mediator = mediator;
            _userManager = userManager;
            _tokenFactory = tokenFactory;
        }

        protected override async Task<SignInResponse> HandleAsync(
            SignInRequest request,
            CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByCredentialsQuery(request.UserCredentials), cancellationToken);

            var token = await _mediator.Send(new CreateTokenCommand(user), cancellationToken);
            
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