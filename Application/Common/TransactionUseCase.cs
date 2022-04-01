using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Common;

public abstract class TransactionUseCase<TRequest> : IRequestHandler<TRequest>
    where TRequest : IRequest
{
    protected readonly IApplicationDbContext ApplicationDb;

    protected TransactionUseCase(IApplicationDbContext applicationDb)
    {
        ApplicationDb = applicationDb;
    }

    protected abstract Task HandleAsync(TRequest request, CancellationToken cancellationToken);

    public Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var executionStrategy = ApplicationDb.CreateExecutionStrategy();

        executionStrategy.Execute(
            async () =>
            {
                await ApplicationDb.BeginTransactionAsync();
                try
                {
                    await HandleAsync(request, cancellationToken);
                    await ApplicationDb.CommitTransactionAsync();
                }
                catch (Exception)
                {
                }
            });

        return Task.FromResult(Unit.Value);
    }
}