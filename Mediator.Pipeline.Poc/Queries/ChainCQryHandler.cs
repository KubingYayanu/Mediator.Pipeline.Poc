using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Models;
using Mediator.Pipeline.Poc.Requests;
using MediatR;

namespace Mediator.Pipeline.Poc.Queries
{
    public class ChainCQryHandler : IRequestHandler<ChainCQryRequest, int>
    {
        private readonly IChainContextWarehouse _contextWarehouse;

        public ChainCQryHandler(IChainContextWarehouse contextWarehouse)
        {
            _contextWarehouse = contextWarehouse;
        }

        public Task<int> Handle(ChainCQryRequest request, CancellationToken cancellationToken)
        {
            var context = _contextWarehouse.GetContext<StageAContext>();
            context.Success = request.Success;
            _contextWarehouse.SetContext(context);

            Console.WriteLine($"Query C, Name: {context.Name}, Age: {context.Age}, "
                              + $"Success: {context.Success}, Stage: {context.Stage}");
            
            return Task.FromResult(3);
        }
    }
}