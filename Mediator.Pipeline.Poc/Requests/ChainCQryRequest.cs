using MediatR;

namespace Mediator.Pipeline.Poc.Requests
{
    public class ChainCQryRequest : IRequest<int>
    {
        public bool Success { get; set; }
    }
}