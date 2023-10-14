using System.Reflection;
using Mediator.Pipeline.Poc.Attributes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Mediator.Pipeline.Poc.Helpers
{
    public static class ServiceCollectionHelper
    {
        public static IServiceCollection AddMediatRAttributedBehaviors(
            this IServiceCollection services,
            Assembly assembly)
            => services.AddMediatRAttributedBehaviors(new[] { assembly });

        public static IServiceCollection AddMediatRAttributedBehaviors(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            var requestsWithAttributes = assemblies
                .Distinct()
                .SelectMany(a => a.DefinedTypes)
                .Where(x => (x.ImplementedInterfaces.Contains(typeof(IRequest))
                             || x.IsAssignableToGenericType(typeof(IRequest<>)))
                            && Attribute.IsDefined(x, typeof(MediatrBehaviorAttribute)));

            foreach (var request in requestsWithAttributes)
            {
                var attributes = request.GetCustomAttributes<MediatrBehaviorAttribute>(false).OrderBy(x => x.Order);
                foreach (var attribute in attributes)
                {
                    services.Add(new ServiceDescriptor(attribute.InterfaceType,
                        attribute.BehaviorType,
                        attribute.ServiceLifetime));
                }
            }

            return services;
        }

        private static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            if (givenType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericType))
            {
                return true;
            }

            if (givenType.IsGenericType
                && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            var baseType = givenType.BaseType;
            return baseType != null && IsAssignableToGenericType(baseType, genericType);
        }
    }
}