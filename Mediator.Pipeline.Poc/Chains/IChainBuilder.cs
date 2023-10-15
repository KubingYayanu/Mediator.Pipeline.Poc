using Mediator.Pipeline.Poc.Enums;
using MediatR;

namespace Mediator.Pipeline.Poc.Chains
{
    public interface IChainBuilder<TResult>
    {
        IMediator Mediator { get; }

        ChainStage Stage { get; }

        List<IRequest<TResult>> Chains { get; }

        IChainBuilder<TResult> Chain<TRequest>(TRequest request)
            where TRequest : IRequest<TResult>, IChainRequest, new();

        IChainBuilder<TResult> Chain<TRequest>(
            Func<TRequest, TRequest> operation)
            where TRequest : IRequest<TResult>, IChainRequest, new();

        IChainBuilder<TResult> Chain<TRequest>()
            where TRequest : IRequest<TResult>, IChainRequest, new();

        ChainBuilder<TResult> StopOn(Func<TResult, bool> stopPredicate);

        Task<TResult> Send(CancellationToken cancellationToken = default);
    }
}