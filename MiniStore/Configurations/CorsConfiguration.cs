using Microsoft.Extensions.DependencyInjection;

namespace MiniStore.Configurations
{
    public static class CorsConfiguration
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(
                options =>
                {
                    options.AddPolicy(
                        name: "AllowOrigin",
                        builder =>
                        {
                            builder
                                .WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                        }
                    );
                    options.AddPolicy(
                        name: "AllowProductionFront",
                        builder =>
                        {
                            builder
                                .WithOrigins("https://mon-app-frontend.com")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                        }
                    );
                }
            );

            return services;
        }
    }

}
