using Mediator.Pipeline.Poc.Requests;
using MediatR;

namespace Mediator.Pipeline.Poc.Queries
{
    public class ChainCQryHandler : IRequestHandler<ChainCQryRequest, int>
    {
        public Task<int> Handle(ChainCQryRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Query C");

            return Task.FromResult(3);
        }
    }
}