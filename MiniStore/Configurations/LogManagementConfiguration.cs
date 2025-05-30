using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace MiniStore.Configurations
{
    public static class LogManagementConfiguration
    {
        public static IServiceCollection LogManagementConfig(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Logtail(Environment.GetEnvironmentVariable("LOGTAIL_SOURCE_TOKEN"))
                .Enrich.FromLogContext()
                .CreateLogger();
            return services;
        }
    }
}
