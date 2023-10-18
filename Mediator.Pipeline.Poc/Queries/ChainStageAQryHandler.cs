using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Models;
using Mediator.Pipeline.Poc.Requests;
using MediatR;

namespace Mediator.Pipeline.Poc.Queries
{
    public class ChainStageAQryHandler : IRequestHandler<ChainStageAQryRequest, int>
    {
        private readonly IChainPipelineContextWarehouse _pipelineContextWarehouse;

        public ChainStageAQryHandler(IChainPipelineContextWarehouse pipelineContextWarehouse)
        {
            _pipelineContextWarehouse = pipelineContextWarehouse;
        }

        public Task<int> Handle(ChainStageAQryRequest request, CancellationToken cancellationToken)
        {
            var context = _pipelineContextWarehouse.GetContext<PipelineAContext>();
            context.Name = request.Name;
            _pipelineContextWarehouse.SetContext(context);

            Console.WriteLine($"Query A, Name: {context.Name}, Age: {context.Age}, "
                              + $"Success: {context.Success}, Stage: {context.Pipeline}");

            return Task.FromResult(1);
        }
    }
}