using Mediator.Pipeline.Poc.Attributes;
using Mediator.Pipeline.Poc.Behaviors;
using MediatR;

namespace Mediator.Pipeline.Poc.Requests
{
    [MediatrBehavior(typeof(PassThroughBehavior<ChainCQryRequest, int>))]
    public class ChainCQryRequest : IRequest<int>
    {
        public bool Success { get; set; }
    }
}