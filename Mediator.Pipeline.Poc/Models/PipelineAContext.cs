using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Enums;

namespace Mediator.Pipeline.Poc.Models
{
    public class PipelineAContext : IChainPipelineContext
    {
        public ChainPipeline Pipeline => ChainPipeline.PipelineA;

        public string Name { get; set; }

        public int Age { get; set; }

        public bool Success { get; set; }
    }
}