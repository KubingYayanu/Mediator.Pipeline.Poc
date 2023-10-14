using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Enums;
using MediatR;

namespace Mediator.Pipeline.Poc.Requests
{
    public class ChainBQryRequest : IRequest<int>, IChainRequest
    {
        public int Age { get; set; }

        public ChainStage Stage { get; set; }
    }
}