using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using MiniStore.Configurations;
using Scalar.AspNetCore;
using Serilog;
using System.Threading.Tasks;



#region Serilog Configuration
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.LogManagementConfig();
builder.Host.UseSerilog();

#endregion


#region Configuration Initialization
DataBaseConfiguration.CreateMemoryDataBase(services);
services.AddControllers();
services.AddCorsConfiguration();
services.AddAutoMapper(typeof(Program));
services.DependencyInjectionConfig();
services.JwtConfig();

#endregion

#region Middleware and Endpoints
OpenApiConfiguration.AddOpenApiWithCustomSchema(builder);
var app = builder.Build();
var env = builder.Environment;

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    IdentityModelEventSource.ShowPII = true;
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.MapGet("/", context =>
    {
        context.Response.Redirect("/scalar", permanent: false);
        return Task.CompletedTask;
    });
    app.UseCors("AllowOrigin");
}
if (env.IsProduction())
{
    app.UseAuthentication();
    app.UseCors("AllowProductionFront");
}

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();

#endregion