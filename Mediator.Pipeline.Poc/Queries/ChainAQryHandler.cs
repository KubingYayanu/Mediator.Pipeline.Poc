using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Models;
using Mediator.Pipeline.Poc.Requests;
using MediatR;

namespace Mediator.Pipeline.Poc.Queries
{
    public class ChainAQryHandler : IRequestHandler<ChainAQryRequest, int>
    {
        private readonly IChainContextWarehouse _contextWarehouse;

        public ChainAQryHandler(IChainContextWarehouse contextWarehouse)
        {
            _contextWarehouse = contextWarehouse;
        }

        public Task<int> Handle(ChainAQryRequest request, CancellationToken cancellationToken)
        {
            var context = _contextWarehouse.GetContext<StageAContext>();
            context.Name = request.Name;
            _contextWarehouse.SetContext(context);

            Console.WriteLine($"Query A, Name: {context.Name}, Age: {context.Age}, "
                              + $"Success: {context.Success}, Stage: {context.Stage}");

            return Task.FromResult(1);
        }
    }
}