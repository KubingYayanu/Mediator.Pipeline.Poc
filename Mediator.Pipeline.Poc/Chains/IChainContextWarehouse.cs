namespace Mediator.Pipeline.Poc.Chains
{
    public interface IChainContextWarehouse
    {
        T GetContext<T>() where T : IChainContext, new();

        void SetContext<T>(T context) where T : IChainContext, new();
    }
}