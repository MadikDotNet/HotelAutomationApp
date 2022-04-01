using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;

namespace HotelAutomationApp.Application.Common;

public abstract class TransactionUseCase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected readonly IApplicationDbContext ApplicationDb;

    protected TransactionUseCase(IApplicationDbContext applicationDb)
    {
        ApplicationDb = applicationDb;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await ApplicationDb.BeginTransactionAsync();
        var response = await HandleAsync(request, cancellationToken);
        await ApplicationDb.CommitTransactionAsync();

        return response;
    }

    protected abstract Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}

public abstract class TransactionUseCase<TRequest> : TransactionUseCase<TRequest, Unit>
    where TRequest : IRequest
{
    protected TransactionUseCase(IApplicationDbContext applicationDb) : base(applicationDb)
    {
    }
    
    protected override async Task<Unit> HandleAsync(TRequest request, CancellationToken cancellationToken)
    {
        await HandleRequestAsync(request, cancellationToken);
        
        return Unit.Value;
    }

    protected abstract Task HandleRequestAsync(TRequest request, CancellationToken cancellationToken);
}
