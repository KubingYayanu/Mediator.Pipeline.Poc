using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Enums;

namespace Mediator.Pipeline.Poc.Models
{
    public class StageAContext : IChainContext
    {
        public ChainStage Stage => ChainStage.StageA;

        public string Name { get; set; }

        public int Age { get; set; }

        public bool Success { get; set; }
    }
}