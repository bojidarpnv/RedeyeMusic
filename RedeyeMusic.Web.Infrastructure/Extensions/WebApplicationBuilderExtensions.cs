namespace RedeyeMusic.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using RedeyeMusic.Data.Models;
    using System.Reflection;
    using static RedeyeMusic.Common.GeneralApplicationConstants;


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
                services.AddScoped(interfaceType, ImplementationType);
            }

        }
        public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder application, string email)
        {
            using IServiceScope scopedServices = application.ApplicationServices.CreateScope();
            IServiceProvider serviceProvider = scopedServices.ServiceProvider;

            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole<Guid>> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdminRoleName))
                {
                    return;
                }
                IdentityRole<Guid> role = new IdentityRole<Guid>(AdminRoleName);
                await roleManager.CreateAsync(role);

                ApplicationUser adminUser = await userManager.FindByEmailAsync(email);

                await userManager.AddToRoleAsync(adminUser, AdminRoleName);
            })
                .GetAwaiter()
                .GetResult();

            return application;
        }

    }
}
