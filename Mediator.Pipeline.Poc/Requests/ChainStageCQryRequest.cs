using Mediator.Pipeline.Poc.Attributes;
using Mediator.Pipeline.Poc.Behaviors;
using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Enums;
using MediatR;

namespace Mediator.Pipeline.Poc.Requests
{
    [MediatrBehavior(typeof(PassThroughBehavior<ChainStageCQryRequest, int>))]
    public class ChainStageCQryRequest : IRequest<int>, IChainStageRequest
    {
        public bool Success { get; set; }

        public ChainPipeline Pipeline { get; set; }
    }
}