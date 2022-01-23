using System;
using FluentValidation;
using HotelAutomationApp.Application.Auth.Queries;
using HotelAutomationApp.Application.Auth.UseCases;
using HotelAutomationApp.Domain.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HotelAutomationApp.Application.Auth.Validators
{
    public class SignInRequestValidator : AbstractValidator<SignInRequest>
    {
        private User _user;
        private bool _passwordVerified;

        public SignInRequestValidator(
            IMediator mediator,
            UserManager<User> userManager)
        {
            // RuleFor(q => q.UserCredentials)
            //     .MustAsync(async (credentials, token) =>
            //     {
            //         try
            //         {
            //             _user = await mediator.Send(new GetUserByCredentialsQuery(credentials), token);
            //         }
            //         catch (Exception exception)
            //         {
            //             return false;
            //         }
            //
            //         return true;
            //     }).WithMessage("User not found");
            //
            // When(q => _user is not null, () =>
            // {
            //     RuleFor(q => q.UserCredentials)
            //         .MustAsync(async (credentials, token) =>
            //             {
            //                 _passwordVerified =
            //                     await userManager.CheckPasswordAsync(_user!, credentials.Password);
            //
            //                 return _passwordVerified;
            //             }
            //         )
            //         .WithMessage("Invalid login or password");
            // });
            //
            // When(q => _passwordVerified, () =>
            // {
            //     RuleFor(q => q.UserCredentials)
            //         .Must(credentials => _user!.CanLogin)
            //         .WithMessage("Can't login, user is blocked");
            // });
        }
    }
}