using Mediator.Pipeline.Poc.Attributes;
using Mediator.Pipeline.Poc.Behaviors;
using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Enums;
using MediatR;

namespace Mediator.Pipeline.Poc.Requests
{
    [MediatrBehavior(typeof(PassThroughBehavior<ChainStageAQryRequest, int>))]
    public class ChainStageAQryRequest : IRequest<int>, IChainStageRequest
    {
        public string Name { get; set; }

        public ChainPipeline Pipeline { get; set; }
    }
}