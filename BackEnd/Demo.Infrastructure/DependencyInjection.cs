using Demo.Application.Constract.Interface;
using Demo.Infrastructure.Context;
using Demo.Presintance.Repoitory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped(typeof(IRepositoryPattern<>), typeof(Presintance.Repoitory.Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
