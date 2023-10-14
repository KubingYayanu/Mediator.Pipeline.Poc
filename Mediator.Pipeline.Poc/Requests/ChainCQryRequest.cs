using Mediator.Pipeline.Poc.Attributes;
using Mediator.Pipeline.Poc.Behaviors;
using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Enums;
using MediatR;

namespace Mediator.Pipeline.Poc.Requests
{
    [MediatrBehavior(typeof(PassThroughBehavior<ChainCQryRequest, int>))]
    public class ChainCQryRequest : IRequest<int>, IChainRequest
    {
        public bool Success { get; set; }

        public ChainStage Stage { get; set; }
    }
}