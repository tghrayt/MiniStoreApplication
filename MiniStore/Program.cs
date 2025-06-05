using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using MiniStore.Configurations;
using Scalar.AspNetCore;
using Serilog;
using System;
using System.Text;
using System.Threading.Tasks;



#region Serilog Configuration
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.LogManagementConfig();
builder.Host.UseSerilog();

#endregion


#region Configuration Initialization
DataBaseConfig.CreateMemoryDataBase(services);
services.AddControllers();
services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:44351", "http://localhost:4200", "http://localhost:7183")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
        });
});
services.AddAutoMapper(typeof(Program));
services.DependencyInjectionConfig();
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes
            (Environment.GetEnvironmentVariable("JWT_TOKEN_CONFIGURATION")) // Fix: Use initialized Configuration  
            ),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

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
}
if (env.IsProduction())
{
    // Production code ....!
}

app.UseAuthentication();
app.UseRouting();
app.UseCors("AllowOrigin");
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

#endregion