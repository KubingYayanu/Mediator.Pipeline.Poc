using Mediator.Pipeline.Poc.Requests;
using MediatR;

namespace Mediator.Pipeline.Poc.Queries
{
    public class ChainBQryHandler : IRequestHandler<ChainBQryRequest, int>
    {
        public Task<int> Handle(ChainBQryRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Query B, Age: {request.Age}, Stage: {request.Stage}");
            
            return Task.FromResult(2);
        }
    }
}