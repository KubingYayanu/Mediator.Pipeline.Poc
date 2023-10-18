using Mediator.Pipeline.Poc.Enums;
using MediatR;

namespace Mediator.Pipeline.Poc.Chains
{
    public interface IChainPipelineBuilder<TResult>
    {
        IMediator Mediator { get; }

        ChainPipeline Pipeline { get; }

        List<IRequest<TResult>> Stages { get; }

        IChainPipelineBuilder<TResult> Stage<TRequest>(TRequest request)
            where TRequest : IRequest<TResult>, IChainStageRequest, new();

        IChainPipelineBuilder<TResult> Stage<TRequest>(
            Func<TRequest, TRequest> operation)
            where TRequest : IRequest<TResult>, IChainStageRequest, new();

        IChainPipelineBuilder<TResult> Stage<TRequest>()
            where TRequest : IRequest<TResult>, IChainStageRequest, new();

        IChainPipelineBuilder<TResult> StopOn(Func<TResult, bool> stopPredicate);

        Task<TResult> Send(CancellationToken cancellationToken = default);
    }
}