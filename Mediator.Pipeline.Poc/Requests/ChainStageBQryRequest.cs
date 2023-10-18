using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Enums;
using MediatR;

namespace Mediator.Pipeline.Poc.Requests
{
    public class ChainStageBQryRequest : IRequest<int>, IChainStageRequest
    {
        public int Age { get; set; }

        public ChainPipeline Pipeline { get; set; }
    }
}