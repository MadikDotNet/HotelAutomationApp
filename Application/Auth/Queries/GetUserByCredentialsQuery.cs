using HotelAutomationApp.Application.Auth.Models;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Auth.Queries
{
    public class GetUserByCredentialsQuery : IRequest<ApplicationUser>
    {
        public GetUserByCredentialsQuery(UserCredentials userCredentials)
        {
            UserCredentials = userCredentials;
        }

        public UserCredentials UserCredentials { get; }
        
        private class Handler : IRequestHandler<GetUserByCredentialsQuery, ApplicationUser>
        {
            private readonly IApplicationDbContext _applicationDb;
            
            public Handler(IApplicationDbContext applicationDb)
            {
                _applicationDb = applicationDb;
            }

            public async Task<ApplicationUser> Handle(GetUserByCredentialsQuery request, CancellationToken cancellationToken)
            {
                var user = await _applicationDb.User.FirstOrDefaultAsync(
                    q => q.UserName == request.UserCredentials.Login, cancellationToken);

                return user ?? throw new InvalidOperationException("User not found");
            }
        }
    }
}