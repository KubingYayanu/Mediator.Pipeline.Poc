using MediatR;

namespace Mediator.Pipeline.Poc.Behaviors
{
    public class PassThroughBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var type = typeof(TRequest);

            Console.WriteLine($"{type.FullName} pass through.");

            var response = await next();
            return response;
        }
    }
}