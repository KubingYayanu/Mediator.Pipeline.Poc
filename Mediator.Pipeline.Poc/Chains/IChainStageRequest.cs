using Mediator.Pipeline.Poc.Enums;

namespace Mediator.Pipeline.Poc.Chains
{
    public interface IChainStageRequest
    {
        ChainPipeline Pipeline { get; set; }
    }
}