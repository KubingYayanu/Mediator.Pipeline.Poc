using System.Runtime.CompilerServices;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Mediator.Pipeline.Poc.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MediatrBehaviorAttribute : Attribute
    {
        public MediatrBehaviorAttribute(
            Type behaviorType,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped,
            [CallerLineNumber] int order = 0)
        {
            BehaviorType = behaviorType;
            ServiceLifetime = serviceLifetime;
            Order = order;
            InterfaceType = behaviorType
                .GetInterfaces()
                .Single(x => x.IsGenericType
                             && x.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>));
        }

        public Type BehaviorType { get; }

        public Type InterfaceType { get; }

        public ServiceLifetime ServiceLifetime { get; }

        public int Order { get; }
    }
}