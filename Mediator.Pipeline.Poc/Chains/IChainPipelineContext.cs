using Mediator.Pipeline.Poc.Enums;

namespace Mediator.Pipeline.Poc.Chains
{
    public interface IChainPipelineContext
    {
        ChainPipeline Pipeline { get; }
    }
}