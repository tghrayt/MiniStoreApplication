using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MiniStore;
using MiniStore.Configurations;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Text;


// 1. Configurer Serilog avant tout
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.LogManagementConfig();
builder.Host.UseSerilog();




// 2. Services (ex-ConfigureServices)
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
services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "V1",
        Title = "MiniStore API",
        Description = "Api for managing a mini store"
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
});
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


// 3. Middleware et endpoints (ex-Configure)


var app = builder.Build();
var env = builder.Environment;

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    IdentityModelEventSource.ShowPII = true;
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
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("swagger/V1/swagger.json", "Ministore Api V1");
    c.RoutePrefix = string.Empty;
});
app.Run();