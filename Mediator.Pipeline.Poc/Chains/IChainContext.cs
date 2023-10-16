using Mediator.Pipeline.Poc.Enums;

namespace Mediator.Pipeline.Poc.Chains
{
    public interface IChainContext
    {
        ChainStage Stage { get; }
    }
}