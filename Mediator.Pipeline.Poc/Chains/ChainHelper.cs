using Mediator.Pipeline.Poc.Enums;
using MediatR;

namespace Mediator.Pipeline.Poc.Chains
{
    public static class ChainHelper
    {
        public static IChainBuilder<TResult> Chain<TRequest, TResult>(
            this IMediator mediator,
            ChainStage stage,
            TRequest request)
            where TRequest : IRequest<TResult>, IChainRequest, new()
        {
            return new ChainBuilder<TResult>(mediator, stage).Chain(request);
        }

        public static IChainBuilder<TResult> Chain<TRequest, TResult>(
            this IMediator mediator,
            ChainStage stage,
            Func<TRequest, TRequest> operation)
            where TRequest : IRequest<TResult>, IChainRequest, new()
        {
            if (operation == null) throw new ArgumentNullException(nameof(operation));

            return new ChainBuilder<TResult>(mediator, stage).Chain(operation);
        }
        
        public static IChainBuilder<TResult> Chain<TRequest, TResult>(
            this IMediator mediator,
            ChainStage stage)
            where TRequest : IRequest<TResult>, IChainRequest, new()
        {
            var request = new TRequest();
            
            return new ChainBuilder<TResult>(mediator, stage).Chain(request);
        }

        public static IChainBuilder<TResult> StopOn<TResult>(
            this IMediator mediator,
            ChainStage stage,
            Func<TResult, bool> stopPredicate)
        {
            return new ChainBuilder<TResult>(mediator, stage, stopPredicate);
        }
    }
}