using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

// Tus namespaces personalizados (ajusta según tu estructura)
using labo_10.Infrastructure;                      // Para AddInfrastructureServices
using labo_10.Application.Interfaces;              // Para IClientContextProvider
using labo_10.Application.Services;

namespace labo_10;

public class ServiceRegistrationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        // Registra el servicio IClientContextProvider para acceder a los headers de cada request
        services.AddScoped<IClientContextProvider, ClientContextProvider>();

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
        });

        return services;
    }
}