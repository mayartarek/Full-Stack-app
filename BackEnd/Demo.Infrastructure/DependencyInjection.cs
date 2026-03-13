using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Demo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Demo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DemoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DemoContext")));
            var dataContext = services.BuildServiceProvider().GetRequiredService<DemoDbContext>();
            dataContext.Database.EnsureCreated();
            return services;
        }
    }
}
