namespace RedeyeMusic.Web.Infrastructure.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;


    public static class WebApplicationBuilderExtensions
    {
        public static void AddApplicationService(this IServiceCollection services, Type serviceType)
        {
            Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
            if (serviceAssembly == null)
            {
                throw new InvalidOperationException("Invalid service type provided!");
            }

            Type[] serviceTypes = serviceAssembly
                .GetTypes()
                .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
                .ToArray();
            foreach (Type ImplementationType in serviceTypes)
            {
                Type? interfaceType = ImplementationType.GetInterface($"I{ImplementationType.Name}");
                if (interfaceType == null)
                {
                    throw new InvalidOperationException($"No interface is provided or the service with name:{ImplementationType.Name}");
                }
                services.AddScoped(serviceType, ImplementationType);
            }

        }

    }
}
