using System.Net.Mail;
using FluentValidation;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Persistence.Interfaces.Context;

namespace HotelAutomationApp.Application.Extensions.FluentValidationExtensions;

public static class RoomValidationExtensions
{
    public static IRuleBuilderOptions<T, string>
        IsExist<T, TEntity>(this IRuleBuilder<T, string> ruleBuilder, IApplicationDbContext applicationDb)
        where TEntity : BaseEntity
    {
        return ruleBuilder.MustAsync(async (field, token) =>
                await applicationDb.AsDbSet<TEntity>().FindAsync(new object[] {field}, token) is { })
            .WithMessage($"{typeof(TEntity).Name} not found");
    }

    public static IRuleBuilderOptions<T, string> IsValidEmail<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.Must(email =>
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }).WithMessage("Email address is invalid");

}