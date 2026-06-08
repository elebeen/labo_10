using Hangfire;
using Hangfire.PostgreSql;
using labo_10;
using labo_10.Infrastructure;
using MediatR;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy => policy.RequireRole("Administrator"));
});

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configurar Hangfire con SQL Server Storage
/*builder.Services.AddHangfire(config =>
{
    config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("Hangfire"));
    
    // Opcional: configuración adicional
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
    config.UseSimpleAssemblyNameTypeSerializer();
    config.UseRecommendedSerializerSettings();
});*/

/*builder.Services.AddHangfireServer();*/

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
/*app.UseHangfireDashboard();*/
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

/*using (var scope = app.Services.CreateScope())
{
    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
    
    // Este job se registrará bajo el ID "limpieza-sistema-recurrente"
    // Se ejecutará cada hora ("Cron.Hourly") resolviendo todas las dependencias mediante MediatR
    RecurringJob.AddOrUpdate(
        "limpieza-sistema-recurrente",
        () => mediator.Send(new labo_10.UseCases.Maintenance.CleanSystemCommand(), CancellationToken.None),
        Cron.Hourly
    );
}*/

app.Run();