using Mediator.Pipeline.Poc.Chains;
using Mediator.Pipeline.Poc.Enums;
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
            await PipelineA();
            await PipelineB();
            await PipelineC();
        }

        private async Task PipelineA()
        {
            var response = await _mediator
                .Chain<ChainAQryRequest, int>(
                    stage: ChainStage.StageA,
                    request: new ChainAQryRequest { Name = "Hi" })
                .Chain<ChainBQryRequest>(
                    request =>
                    {
                        request.Age = 2;
                        return request;
                    })
                .Chain(new ChainCQryRequest { Success = true })
                .StopOn(x => x == 3)
                .Send();

            Console.WriteLine($"Final result on pipeline A: {response}");
        }

        private async Task PipelineB()
        {
            var response = await _mediator
                .Chain<ChainAQryRequest, int>(
                    stage: ChainStage.StageA,
                    operation: request =>
                    {
                        request.Name = "Hello";
                        return request;
                    })
                .StopOn(x => x == 2)
                .Chain(new ChainBQryRequest { Age = 10 })
                .Chain(new ChainCQryRequest())
                .Send();

            Console.WriteLine($"Final result on pipeline B: {response}");
        }

        private async Task PipelineC()
        {
            var response = await _mediator
                .StopOn<int>(
                    stage: ChainStage.StageA,
                    x => x == 3)
                .Chain<ChainAQryRequest>(
                    operation: request =>
                    {
                        request.Name = "Hello";
                        return request;
                    })
                .StopOn(x => x == 2)
                .Chain(new ChainBQryRequest { Age = 10 })
                .Chain(new ChainCQryRequest())
                .Send();

            Console.WriteLine($"Final result on pipeline B: {response}");
        }
    }
}