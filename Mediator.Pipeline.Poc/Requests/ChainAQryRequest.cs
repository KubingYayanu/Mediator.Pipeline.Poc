using MediatR;

namespace Mediator.Pipeline.Poc.Requests
{
    public class ChainAQryRequest : IRequest<int>
    {
        public string Name { get; set; }
    }
}