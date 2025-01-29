using Magalog.Application.AutoMapper;
using Magalog.Application.Services;
using Magalog.Application.Services.Interfaces;
using Magalog.Data.Context;
using Magalog.Data.Repositories;
using Magalog.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Magalog.IoC
{
    public static class DependencyInjection
    {

        public static IServiceCollection ResolveDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            });

            services.AddAutoMapper(typeof(DomainToDtoMappingProfile));

            services.AddScoped<ILegacyProcessingService, LegacyProcessingService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

    }
}
