using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MiniStore.Helper;
using System.Text;

namespace MiniStore.Configurations
{
    public static class JwtConfiguration
    {
        public static IServiceCollection JwtConfig(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = "youssef-api",
                        ValidAudience = "youssef-client",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes
                        (EnvirenmentVariablesHelper.GetEnvironmentVariable("JWT_TOKEN_CONFIGURATION")))
                    };
                });
            return services;
        }
    }
}
