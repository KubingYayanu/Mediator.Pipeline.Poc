using Mediator.Pipeline.Poc.Enums;
using MediatR;

namespace Mediator.Pipeline.Poc.Chains
{
    public static class ChainPipelineHelper
    {
        public static IChainPipelineBuilder<TResult> Stage<TRequest, TResult>(
            this IMediator mediator,
            ChainPipeline pipeline,
            TRequest request)
            where TRequest : IRequest<TResult>, IChainStageRequest, new()
        {
            return new ChainPipelineBuilder<TResult>(mediator, pipeline).Stage(request);
        }

        public static IChainPipelineBuilder<TResult> Stage<TRequest, TResult>(
            this IMediator mediator,
            ChainPipeline pipeline,
            Func<TRequest, TRequest> operation)
            where TRequest : IRequest<TResult>, IChainStageRequest, new()
        {
            if (operation == null) throw new ArgumentNullException(nameof(operation));

            return new ChainPipelineBuilder<TResult>(mediator, pipeline).Stage(operation);
        }
        
        public static IChainPipelineBuilder<TResult> Stage<TRequest, TResult>(
            this IMediator mediator,
            ChainPipeline pipeline)
            where TRequest : IRequest<TResult>, IChainStageRequest, new()
        {
            var request = new TRequest();
            
            return new ChainPipelineBuilder<TResult>(mediator, pipeline).Stage(request);
        }

        public static IChainPipelineBuilder<TResult> StopOn<TResult>(
            this IMediator mediator,
            ChainPipeline pipeline,
            Func<TResult, bool> stopPredicate)
        {
            return new ChainPipelineBuilder<TResult>(mediator, pipeline, stopPredicate);
        }
    }
}