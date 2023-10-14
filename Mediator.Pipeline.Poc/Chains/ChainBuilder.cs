using Mediator.Pipeline.Poc.Enums;
using MediatR;

namespace Mediator.Pipeline.Poc.Chains
{
    public class ChainBuilder<TResult> : IChainBuilder<TResult>
    {
        public ChainBuilder(IMediator mediator, ChainStage stage)
        {
            Mediator = mediator;
            Stage = stage;
            Chains ??= new List<IRequest<TResult>>();
            StopPredicates ??= new StopPredicates<TResult>();
        }

        public ChainBuilder(IMediator mediator, ChainStage stage, Func<TResult, bool> stopPredicate)
            : this(mediator, stage)
        {
            StopOn(stopPredicate);
        }

        public IMediator Mediator { get; }

        public ChainStage Stage { get; }

        public List<IRequest<TResult>> Chains { get; }

        private StopPredicates<TResult> StopPredicates { get; }

        public IChainBuilder<TResult> Chain<TRequest>(TRequest request)
            where TRequest : IRequest<TResult>, IChainRequest, new()
        {
            request.Stage = Stage;
            Chains.Add(request);
            return this;
        }

        public IChainBuilder<TResult> Chain<TRequest>(
            Func<TRequest, TRequest> operation)
            where TRequest : IRequest<TResult>, IChainRequest, new()
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var request = new TRequest();

            return Chain(operation(request));
        }

        public ChainBuilder<TResult> StopOn(Func<TResult, bool> stopPredicate)
        {
            StopPredicate<TResult> predicate = result => stopPredicate(result);
            StopPredicates.Add(predicate);
            return this;
        }

        public async Task<TResult> Send(CancellationToken cancellationToken = default)
        {
            TResult response = default;
            foreach (var chain in Chains)
            {
                response = await Mediator.Send(chain, cancellationToken);
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