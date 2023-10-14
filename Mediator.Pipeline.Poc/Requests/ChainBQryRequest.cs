using MediatR;

namespace Mediator.Pipeline.Poc.Requests
{
    public class ChainBQryRequest : IRequest<int>
    {
        public int Age { get; set; }
    }
}