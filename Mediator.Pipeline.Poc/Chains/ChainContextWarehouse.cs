using Mediator.Pipeline.Poc.Enums;
using Mediator.Pipeline.Poc.Models;

namespace Mediator.Pipeline.Poc.Chains
{
    public class ChainContextWarehouse : IChainContextWarehouse
    {
        private static readonly Dictionary<Type, ChainStage> _map = new Dictionary<Type, ChainStage>
        {
            { typeof(StageAContext), ChainStage.StageA }
        };

        private readonly Dictionary<ChainStage, IChainContext> _contexts = new();

        public T GetContext<T>() where T : IChainContext, new()
        {
            var type = typeof(T);
            var stage = _map.GetValueOrDefault(type);
            var context = _contexts.GetValueOrDefault(stage) ?? new T();
            return (T)context;
        }

        public void SetContext<T>(T context) where T : IChainContext, new()
        {
            var type = typeof(T);
            var stage = _map.GetValueOrDefault(type);
            if (!_contexts.TryAdd(stage, context))
            {
                _contexts[stage] = context;
            }
        }
    }
}