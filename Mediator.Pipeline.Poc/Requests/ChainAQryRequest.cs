using Mediator.Pipeline.Poc.Attributes;
using Mediator.Pipeline.Poc.Behaviors;
using MediatR;

namespace Mediator.Pipeline.Poc.Requests
{
    [MediatrBehavior(typeof(PassThroughBehavior<ChainAQryRequest, int>))]
    public class ChainAQryRequest : IRequest<int>
    {
        public string Name { get; set; }
    }
}