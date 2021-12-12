using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace HotelAutomation.Application.Common
{
    /// <summary>
    /// Entry point to application
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class UseCase<TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest : IRequest<TResult>
    {
        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
        {
            return await HandleAsync(request, cancellationToken);
        }

        protected abstract Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class UseCase<TRequest> : UseCase<TRequest, Unit> where TRequest : IRequest
    {
        protected override async Task<Unit> HandleAsync(TRequest request, CancellationToken cancellationToken)
        {
            await HandleRequestAsync(request, cancellationToken);
            return Unit.Value;
        }

        protected abstract Task HandleRequestAsync(TRequest request, CancellationToken cancellationToken);
    }
}