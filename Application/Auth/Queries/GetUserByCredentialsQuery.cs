using System;
using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Application.Auth.Models;
using HotelAutomationApp.Domain.Models.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Auth.Queries
{
    public class GetUserByCredentialsQuery : IRequest<User>
    {
        public GetUserByCredentialsQuery(UserCredentials userCredentials)
        {
            UserCredentials = userCredentials;
        }

        public UserCredentials UserCredentials { get; }
        
        private class Handler : IRequestHandler<GetUserByCredentialsQuery, User>
        {
            private readonly IDbContext _db;
            
            public Handler(IDbContext db)
            {
                _db = db;
            }

            public async Task<User> Handle(GetUserByCredentialsQuery request, CancellationToken cancellationToken)
            {
                var user = await _db.Users.FirstOrDefaultAsync(
                    q => q.UserName == request.UserCredentials.Login, cancellationToken);

                return user ?? throw new InvalidOperationException("User not found");
            }
        }
    }
}