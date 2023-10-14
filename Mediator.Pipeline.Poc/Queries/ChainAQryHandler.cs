using Mediator.Pipeline.Poc.Requests;
using MediatR;

namespace Mediator.Pipeline.Poc.Queries
{
    public class ChainAQryHandler : IRequestHandler<ChainAQryRequest, int>
    {
        public Task<int> Handle(ChainAQryRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Query A, Name: {request.Name}, Stage: {request.Stage}");

            return Task.FromResult(1);
        }
    }
}