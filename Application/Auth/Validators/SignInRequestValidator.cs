using FluentValidation;
using HotelAutomationApp.Application.Auth.UseCases;
using HotelAutomationApp.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace HotelAutomationApp.Application.Auth.Validators
{
    public class SignInRequestValidator : AbstractValidator<SignInRequest>
    {
        private ApplicationUser _applicationUser;
        private bool _passwordVerified;

        public SignInRequestValidator(
            UserManager<ApplicationUser> userManager)
        {
            RuleFor(q => q.Login)
                .MustAsync(async (login, token) =>
                {
                    if (!token.IsCancellationRequested)
                    {
                        _applicationUser = await userManager.FindByNameAsync(login);
                    }

                    return _applicationUser is not null;
                })
                .WithMessage("User not found");

            When(q => _applicationUser is not null, () =>
            {
                RuleFor(q => q.Password)
                    .MustAsync(async (password, token) =>
                        {
                            _passwordVerified =
                                await userManager.CheckPasswordAsync(_applicationUser!, password);

                            return _passwordVerified;
                        }
                    )
                    .WithMessage("Invalid login or password");
            });

            When(q => _passwordVerified, () =>
            {
                RuleFor(q => q.Login)
                    .Must(credentials => _applicationUser!.CanLogin)
                    .WithMessage("Can't login, user is blocked");
            });
        }
    }
}