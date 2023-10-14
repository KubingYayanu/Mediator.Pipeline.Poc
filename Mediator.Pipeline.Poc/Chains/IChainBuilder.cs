using MediatR;

namespace Mediator.Pipeline.Poc.Chains
{
    public interface IChainBuilder<TResult>
    {
        IMediator Mediator { get; }

        List<IRequest<TResult>> Chains { get; }

        IChainBuilder<TResult> Chain<TRequest>(TRequest request)
            where TRequest : IRequest<TResult>, new();

        IChainBuilder<TResult> Chain<TRequest>(
            Func<TRequest, TRequest> operation)
            where TRequest : IRequest<TResult>, new();

        ChainBuilder<TResult> StopOn(Func<TResult, bool> stopPredicate);

        Task<TResult> Send(CancellationToken cancellationToken = default);
    }
}