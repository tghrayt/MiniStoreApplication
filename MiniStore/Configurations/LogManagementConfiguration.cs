using Microsoft.Extensions.DependencyInjection;
using MiniStore.Helper;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.Net.Http;

namespace MiniStore.Configurations
{
    public static class LogManagementConfiguration
    {
        public static IServiceCollection LogManagementConfig(this IServiceCollection services)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Console.Error.WriteLine("Serilog error: " + msg));

            var httpClient = new HttpClient(new BearerTokenHandler(Environment.GetEnvironmentVariable("LOGTAIL_SOURCE_TOKEN")))
            {
                Timeout = TimeSpan.FromSeconds(10)
            };

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                //.MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.Console()
                .WriteTo.Http(
                    requestUri: Environment.GetEnvironmentVariable("LOGTAIL_REQUEST_URI"),
                    queueLimitBytes: 1000000,
                    httpClient: new DefaultHttpClient(httpClient),
                    textFormatter: new JsonFormatter(renderMessage: true),
                    period: TimeSpan.FromSeconds(2),
                    restrictedToMinimumLevel: LogEventLevel.Information
                )
                .CreateLogger();
            return services;
        }
    }
}
