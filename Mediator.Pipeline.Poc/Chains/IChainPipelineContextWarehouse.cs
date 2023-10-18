namespace Mediator.Pipeline.Poc.Chains
{
    public interface IChainPipelineContextWarehouse
    {
        T GetContext<T>() where T : IChainPipelineContext, new();

        void SetContext<T>(T context) where T : IChainPipelineContext, new();
    }
}