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

    public class ChainBuilder<TResult> : IChainBuilder<TResult>
    {
        public ChainBuilder(IMediator mediator)
        {
            Mediator = mediator;
            Chains ??= new List<IRequest<TResult>>();
            StopPredicates ??= new StopPredicates<TResult>();
        }

        public ChainBuilder(IMediator mediator, Func<TResult, bool> stopPredicate)
            : this(mediator)
        {
            StopOn(stopPredicate);
        }

        public IMediator Mediator { get; }

        public List<IRequest<TResult>> Chains { get; }

        private StopPredicates<TResult> StopPredicates { get; }

        public IChainBuilder<TResult> Chain<TRequest>(TRequest request)
            where TRequest : IRequest<TResult>, new()
        {
            Chains.Add(request);
            return this;
        }

        public IChainBuilder<TResult> Chain<TRequest>(
            Func<TRequest, TRequest> operation)
            where TRequest : IRequest<TResult>, new()
        {
            if (operation == null) throw new ArgumentNullException(nameof(operation));

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