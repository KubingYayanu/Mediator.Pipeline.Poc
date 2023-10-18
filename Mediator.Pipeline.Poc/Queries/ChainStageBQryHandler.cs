using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Models;
using Mediator.Pipeline.Poc.Requests;
using MediatR;

namespace Mediator.Pipeline.Poc.Queries
{
    public class ChainStageBQryHandler : IRequestHandler<ChainStageBQryRequest, int>
    {
        private readonly IChainPipelineContextWarehouse _pipelineContextWarehouse;

        public ChainStageBQryHandler(IChainPipelineContextWarehouse pipelineContextWarehouse)
        {
            _pipelineContextWarehouse = pipelineContextWarehouse;
        }

        public Task<int> Handle(ChainStageBQryRequest request, CancellationToken cancellationToken)
        {
            var context = _pipelineContextWarehouse.GetContext<PipelineAContext>();
            context.Age = request.Age;
            _pipelineContextWarehouse.SetContext(context);

            Console.WriteLine($"Query B, Name: {context.Name}, Age: {context.Age}, "
                              + $"Success: {context.Success}, Stage: {context.Pipeline}");

            return Task.FromResult(2);
        }
    }
}