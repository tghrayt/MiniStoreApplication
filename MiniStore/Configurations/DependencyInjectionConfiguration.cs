using Microsoft.Extensions.DependencyInjection;
using MiniStore.Repositories;
using MiniStore.Services;

namespace MiniStore.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection DependencyInjectionConfig(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            return services;
        } 
    }
}
