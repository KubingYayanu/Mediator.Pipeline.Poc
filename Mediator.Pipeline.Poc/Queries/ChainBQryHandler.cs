using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Models;
using Mediator.Pipeline.Poc.Requests;
using MediatR;

namespace Mediator.Pipeline.Poc.Queries
{
    public class ChainBQryHandler : IRequestHandler<ChainBQryRequest, int>
    {
        private readonly IChainContextWarehouse _contextWarehouse;

        public ChainBQryHandler(IChainContextWarehouse contextWarehouse)
        {
            _contextWarehouse = contextWarehouse;
        }

        public Task<int> Handle(ChainBQryRequest request, CancellationToken cancellationToken)
        {
            var context = _contextWarehouse.GetContext<StageAContext>();
            context.Age = request.Age;
            _contextWarehouse.SetContext(context);

            Console.WriteLine($"Query B, Name: {context.Name}, Age: {context.Age}, "
                              + $"Success: {context.Success}, Stage: {context.Stage}");

            return Task.FromResult(2);
        }
    }
}