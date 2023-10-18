using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Models;
using Mediator.Pipeline.Poc.Requests;
using MediatR;

namespace Mediator.Pipeline.Poc.Queries
{
    public class ChainStageCQryHandler : IRequestHandler<ChainStageCQryRequest, int>
    {
        private readonly IChainPipelineContextWarehouse _pipelineContextWarehouse;

        public ChainStageCQryHandler(IChainPipelineContextWarehouse pipelineContextWarehouse)
        {
            _pipelineContextWarehouse = pipelineContextWarehouse;
        }

        public Task<int> Handle(ChainStageCQryRequest request, CancellationToken cancellationToken)
        {
            var context = _pipelineContextWarehouse.GetContext<PipelineAContext>();
            context.Success = request.Success;
            _pipelineContextWarehouse.SetContext(context);

            Console.WriteLine($"Query C, Name: {context.Name}, Age: {context.Age}, "
                              + $"Success: {context.Success}, Stage: {context.Pipeline}");
            
            return Task.FromResult(3);
        }
    }
}