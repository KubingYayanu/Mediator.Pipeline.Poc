using MediatR;

namespace Mediator.Pipeline.Poc.Chains
{
    public static class ChainHelper
    {
        public static IChainBuilder<TResult> Chain<TRequest, TResult>(
            this IMediator mediator,
            TRequest request)
            where TRequest : IRequest<TResult>, new()
        {
            return new ChainBuilder<TResult>(mediator).Chain(request);
        }

        public static IChainBuilder<TResult> Chain<TRequest, TResult>(
            this IMediator mediator,
            TRequest request,
            Func<TRequest, TRequest> operation)
            where TRequest : IRequest<TResult>, new()
        {
            if (operation == null) throw new ArgumentNullException(nameof(operation));

            return new ChainBuilder<TResult>(mediator).Chain(operation(request));
        }

        public static IChainBuilder<TResult> StopOn<TResult>(
            this IMediator mediator,
            Func<TResult, bool> stopPredicate)
        {
            return new ChainBuilder<TResult>(mediator, stopPredicate);
        }
    }
}