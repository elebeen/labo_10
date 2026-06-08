using labo_10.Domain.Interfaces;
using labo_10.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using labo_10.Infrastructure.Services;

namespace labo_10.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Database Connection
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString);
        });

        //ServicesRegister
        //services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<NotificationService>();
        //services.AddScoped<IFileService, FileService>();
        //services.AddScoped<IUploadFileToAzureStorageService, UploadFileToAzureStorageService>();
        //services.AddScoped<IActivityService, ActivityService>();

        return services;
    }
}