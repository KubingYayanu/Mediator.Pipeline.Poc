using Mediator.Pipeline.Poc.Enums;
using Mediator.Pipeline.Poc.Models;

namespace Mediator.Pipeline.Poc.Chains
{
    public class ChainPipelineContextWarehouse : IChainPipelineContextWarehouse
    {
        private static readonly Dictionary<Type, ChainPipeline> _map = new()
        {
            { typeof(PipelineAContext), ChainPipeline.PipelineA }
        };

        private readonly Dictionary<ChainPipeline, IChainPipelineContext> _contexts = new();

        public T GetContext<T>() where T : IChainPipelineContext, new()
        {
            var type = typeof(T);
            var pipeline = _map.GetValueOrDefault(type);
            var context = _contexts.GetValueOrDefault(pipeline) ?? new T();
            return (T)context;
        }

        public void SetContext<T>(T context) where T : IChainPipelineContext, new()
        {
            var type = typeof(T);
            var pipeline = _map.GetValueOrDefault(type);
            if (!_contexts.TryAdd(pipeline, context))
            {
                _contexts[pipeline] = context;
            }
        }
    }
}