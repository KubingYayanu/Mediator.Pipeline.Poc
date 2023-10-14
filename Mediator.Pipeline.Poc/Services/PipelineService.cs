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
            var response = await _mediator
                .Chain<ChainAQryRequest, int>(new ChainAQryRequest { Name = "Hi" })
                .Chain<ChainBQryRequest>(
                    request =>
                    {
                        request.Age = 2;
                        return request;
                    })
                .Chain(new ChainCQryRequest { Success = true })
                .StopOn(x => x == 3)
                .Send();

            Console.WriteLine($"Final Result: {response}");
        }
    }
}