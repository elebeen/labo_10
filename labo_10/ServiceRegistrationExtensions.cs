using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Hangfire;

// Tus namespaces personalizados (ajusta según tu estructura)
using labo_10.Infrastructure;
using labo_10.UseCases.Auth.Commands;
using MediatR; // Para AddInfrastructureServices

namespace labo_10;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        // Registro de servicios de Infraestructura
        services.AddInfrastructureServices(configuration);

        // Configuración de autenticación JWT
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = "languagebridgesolutions.com",
                    ValidAudience = "languagebridgesolutions.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
                };
            });

        // Habilitar controladores
        services.AddControllers();

        // Habilitar Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            // Puedes personalizar las opciones de Swagger aquí, por ejemplo, agregar una descripción o la versión de la API
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ML API",
                Version = "v1",
                Description = "API para gestionar recursos."
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Token"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
        
        services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssemblies(
                Assembly.GetExecutingAssembly(),
                typeof(LoginUserCommand).Assembly,
                typeof(RegisterUserCommand).Assembly
            );
            cfg.LicenseKey = configuration["LicenseKey"];
        });
        return services;
    }
}