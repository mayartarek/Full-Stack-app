
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace Demo.Application
{
    public static class AppicationRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Explicitly specify the extension method to resolve ambiguity
          //  services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
