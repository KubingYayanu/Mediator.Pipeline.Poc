using Mediator.Pipeline.Poc.Enums;

namespace Mediator.Pipeline.Poc.Chains
{
    public interface IChainRequest
    {
        ChainStage Stage { get; set; }
    }
}