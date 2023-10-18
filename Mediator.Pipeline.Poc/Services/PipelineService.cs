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
            await PipelineD();
        }

        private async Task PipelineA()
        {
            var response = await _mediator
                .Stage<ChainStageAQryRequest, int>(
                    pipeline: ChainPipeline.PipelineA,
                    request: new ChainStageAQryRequest { Name = "Hi" })
                .Stage<ChainStageBQryRequest>(
                    request =>
                    {
                        request.Age = 2;
                        return request;
                    })
                .Stage(new ChainStageCQryRequest { Success = true })
                .StopOn(x => x == 3)
                .Send();

            Console.WriteLine($"Final result on pipeline A: {response}");
        }

        private async Task PipelineB()
        {
            var response = await _mediator
                .Stage<ChainStageAQryRequest, int>(
                    pipeline: ChainPipeline.PipelineB,
                    operation: request =>
                    {
                        request.Name = "Hello";
                        return request;
                    })
                .StopOn(x => x == 2)
                .Stage(new ChainStageBQryRequest { Age = 10 })
                .Stage(new ChainStageCQryRequest())
                .Send();

            Console.WriteLine($"Final result on pipeline B: {response}");
        }

        private async Task PipelineC()
        {
            var response = await _mediator
                .StopOn<int>(
                    pipeline: ChainPipeline.PipelineC,
                    x => x == 3)
                .Stage<ChainStageAQryRequest>(
                    operation: request =>
                    {
                        request.Name = "Hello";
                        return request;
                    })
                .StopOn(x => x == 2)
                .Stage(new ChainStageBQryRequest { Age = 10 })
                .Stage(new ChainStageCQryRequest())
                .Send();

            Console.WriteLine($"Final result on pipeline C: {response}");
        }

        private async Task PipelineD()
        {
            var response = await _mediator
                .Stage<ChainStageAQryRequest, int>(pipeline: ChainPipeline.PipelineD)
                .Stage<ChainStageBQryRequest>()
                .Stage(new ChainStageCQryRequest())
                .StopOn(x => x == 2)
                .Send();

            Console.WriteLine($"Final result on pipeline D: {response}");
        }
    }
}