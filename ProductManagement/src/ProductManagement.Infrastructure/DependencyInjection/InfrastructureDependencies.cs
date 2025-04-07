using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Application.Interfaces.Services;
using ProductManagement.Infrastructure.Persistence;
using ProductManagement.Infrastructure.Repositories;
using ProductManagement.Infrastructure.Services;

namespace ProductManagement.Infrastructure.DependencyInjection
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services,configuration);
            AddRepositories(services);
            AddServices(services);
            return services;
        }

        private static IServiceCollection AddDbContext( this IServiceCollection services ,IConfiguration configuration )
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService,StorageService>();
            return services;
        }
    }
}
