using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Auth.Queries
{
    public class GetUserByCredentialsQuery : IRequest<ApplicationUser>
    {
        public GetUserByCredentialsQuery(string login, string password)
        {
            Login = login;
            Password = password;
        }
        
        public string Login { get; }
        public string Password { get; }

        private class Handler : IRequestHandler<GetUserByCredentialsQuery, ApplicationUser>
        {
            private readonly IApplicationDbContext _applicationDb;

            public Handler(IApplicationDbContext applicationDb)
            {
                _applicationDb = applicationDb;
            }

            public async Task<ApplicationUser> Handle(GetUserByCredentialsQuery request,
                CancellationToken cancellationToken)
            {
                var user = await _applicationDb.User.FirstOrDefaultAsync(
                    q => q.UserName == request.Login, cancellationToken);

                return user ?? throw new InvalidOperationException("User not found");
            }
        }
    }
}