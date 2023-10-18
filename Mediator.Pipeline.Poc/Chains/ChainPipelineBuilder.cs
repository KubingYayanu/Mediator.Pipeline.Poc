using Mediator.Pipeline.Poc.Enums;
using MediatR;

namespace Mediator.Pipeline.Poc.Chains
{
    public class ChainPipelineBuilder<TResult> : IChainPipelineBuilder<TResult>
    {
        public ChainPipelineBuilder(IMediator mediator, ChainPipeline pipeline)
        {
            Mediator = mediator;
            Pipeline = pipeline;
            Stages ??= new List<IRequest<TResult>>();
            StopPredicates ??= new StopPredicates<TResult>();
        }

        public ChainPipelineBuilder(IMediator mediator, ChainPipeline pipeline, Func<TResult, bool> stopPredicate)
            : this(mediator, pipeline)
        {
            StopOn(stopPredicate);
        }

        public IMediator Mediator { get; }

        public ChainPipeline Pipeline { get; }

        public List<IRequest<TResult>> Stages { get; }

        private StopPredicates<TResult> StopPredicates { get; }

        public IChainPipelineBuilder<TResult> Stage<TRequest>(TRequest request)
            where TRequest : IRequest<TResult>, IChainStageRequest, new()
        {
            request.Pipeline = Pipeline;
            Stages.Add(request);
            return this;
        }

        public IChainPipelineBuilder<TResult> Stage<TRequest>(
            Func<TRequest, TRequest> operation)
            where TRequest : IRequest<TResult>, IChainStageRequest, new()
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var request = new TRequest();

            return Stage(operation(request));
        }

        public IChainPipelineBuilder<TResult> Stage<TRequest>()
            where TRequest : IRequest<TResult>, IChainStageRequest, new()
        {
            var request = new TRequest();

            return Stage(request);
        }

        public IChainPipelineBuilder<TResult> StopOn(Func<TResult, bool> stopPredicate)
        {
            StopPredicate<TResult> predicate = result => stopPredicate(result);
            StopPredicates.Add(predicate);
            return this;
        }

        public async Task<TResult> Send(CancellationToken cancellationToken = default)
        {
            TResult response = default;
            foreach (var stage in Stages)
            {
                response = await Mediator.Send(stage, cancellationToken);
                if (StopPredicates.AnyMatch(response))
                {
                    Console.WriteLine($"Stop on Result: {response}");
                    return response;
                }
            }

            return response;
        }
    }
}