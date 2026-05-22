using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using labo_10.Application.Interfaces;
using labo_10.Infrastructure.Repositories;
using labo_10.Infrastructure.Services;

namespace labo_10.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Database Connection
        services.AddDbContext<DbContext>((serviceProvider, options) =>
        {
            var connectionString = configuration.GetConnectionString(name: "DefaultConnection");
            options.UseNpgsql(connectionString);
        });

        //ServicesRegister
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IUploadFileToAzureStorageService, UploadFileToAzureStorageService>();
        services.AddScoped<IActivityService, ActivityService>();

        return services;
    }
}