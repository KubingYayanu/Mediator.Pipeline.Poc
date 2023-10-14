using Mediator.Pipeline.Poc.Attributes;
using Mediator.Pipeline.Poc.Behaviors;
using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Enums;
using MediatR;

namespace Mediator.Pipeline.Poc.Requests
{
    [MediatrBehavior(typeof(PassThroughBehavior<ChainAQryRequest, int>))]
    public class ChainAQryRequest : IRequest<int>, IChainRequest
    {
        public string Name { get; set; }

        public ChainStage Stage => ChainStage.StageA;
    }
}