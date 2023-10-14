using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Requests;
using MediatR;

namespace Mediator.Pipeline.Poc.Services
{
    public class PipelineService : IPipelineService
    {
        private readonly IMediator _mediator;

        public PipelineService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Run()
        {
            var requestA = new ChainAQryRequest();
            var response = await _mediator
                .Chain<ChainAQryRequest, int>(requestA)
                .Chain<ChainBQryRequest>(
                    request =>
                    {
                        request.Age = 2;
                        return request;
                    })
                .StopOn(x => x == 1)
                .Send();

            Console.WriteLine($"Final Result: {response}");
        }
    }
}